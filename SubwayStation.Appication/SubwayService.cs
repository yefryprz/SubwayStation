using AutoMapper;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SubwayStation.Application.Contracts;
using SubwayStation.Contracts;
using SubwayStation.Domain;
using SubwayStation.Domain.Constans;
using SubwayStation.Domain.DTOs;
using SubwayStation.Domain.DTOs.Seed;
using SubwayStation.Domain.Entities;
using SubwayStation.Domain.Repository.Contracts;
using SubwayStation.Domain.ViewModels;
using System.Net.Http.Json;
using System.Security.Claims;

namespace SubwayStation.Appication
{
    public class SubwayService : ISubwayService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheService _cacheService;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly AppSetting _settings;
        
        public SubwayService(IMapper mapper, 
                             IOptions<AppSetting> options, 
                             IHttpContextAccessor httpContextAccessor, 
                             ICacheService cacheService, 
                             IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _settings = options.Value;
            _cacheService = cacheService;
            _httpContextAccessor = httpContextAccessor;
            _repositoryFactory = repositoryFactory;
        }

        public async Task<string> CalcDistances(DistancesViewModel distances)
        {
            var repo = _repositoryFactory.GetDataRepository<Subways>();
            var subways = await repo.GetAllAsync(
                x => x.Select(s => _mapper.Map<GeometricDTO>(s.Geometric)), 
                f => f.ObjectId == distances.ObjectIdFrom | f.ObjectId == distances.ObjectIdTo
            );

            var fromGeoResult = subways.First();
            var toGeoResult = subways.Last();

            var srcGeo = new GeoCoordinate(fromGeoResult.Latitude, fromGeoResult.Longitude);
            var destGeo = new GeoCoordinate(toGeoResult.Latitude, toGeoResult.Longitude);

            return $"Distances: {srcGeo.GetDistanceTo(destGeo)} meters";
        }

        public async Task<PageListDTO<SubwayDTO>> GetAllSybways(QueryFilterDTO queryFilter)
        {
            var repo = _repositoryFactory.GetDataRepository<Subways>();
            List<SubwayDTO> stations = new();

            //first call get data from sql, otherwise from them from cache
            if (!_cacheService.GetCacheValue(CacheKeys.RecentView, ref stations))
            {
                var query = await repo.GetAllAsync(x => _mapper.ProjectTo<SubwayDTO>(x));

                //60 second as a durection, after that refresh cache with new data
                await _cacheService.SetCacheValueAsync(CacheKeys.RecentView, query, TimeSpan.FromSeconds(60));
                stations = query.ToList();
            }

            var pagedResult = Paginator(queryFilter, stations);

            return pagedResult;
        }

        public async Task<PageListDTO<FrequentlyDTO>> GetFrequentlyStations(QueryFilterDTO queryFilter)
        {
            var repo = _repositoryFactory.GetDataRepository<Frequently>();
            List<FrequentlyDTO> frequently = new();

            Claim userClaim = _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);

            //first call get data from sql, otherwise from them from cache
            if (!_cacheService.GetCacheValue(CacheKeys.Frequently, ref frequently))
            {
                var query = await repo.GetAllAsync(x => _mapper.ProjectTo<FrequentlyDTO>(x), f => f.UserId == Guid.Parse(userClaim.Value));
                await _cacheService.SetCacheValueAsync(CacheKeys.Frequently, query, TimeSpan.FromSeconds(60));
                frequently = query.ToList();
            }

            var pagedResult = Paginator(queryFilter, frequently);

            return pagedResult;
        }

        public async Task<bool> SaveFrequentlyStations(int subwayId)
        {
            var repo = _repositoryFactory.GetDataRepository<Frequently>();
            var entryResult = await repo.AddAsync(new Frequently { ObjectId = subwayId });
            return entryResult is not null;
        }

        public async Task<bool> PopulateTable()
        {
            var repo = _repositoryFactory.GetDataRepository<Subways>();
            var repoGeom = _repositoryFactory.GetDataRepository<Geometric>();

            //Valid if table have rows, if not call services to download data
            var isSeeded = await repo.GetAsync(x => x.Select(s => s));
            if (isSeeded is not null) return false;

            using var client = new HttpClient();
            var stationsResult = await client.GetAsync(_settings.SubwayEndPoint);

            if (stationsResult.IsSuccessStatusCode)
            {
                var values = await stationsResult.Content.ReadFromJsonAsync<List<SubwaysSeedDTO>>();

                foreach (SubwaysSeedDTO item in values)
                {
                    var geomResult = await repoGeom.AddAsync(_mapper.Map<Geometric>(item.The_geom));
                    item.GeometricId = geomResult.GeometricId;

                    await repo.AddAsync(_mapper.Map<Subways>(item));
                }
            }

            return true;
        }

        /// <summary>
        /// Paginator
        /// </summary>
        /// <typeparam name="T">generic</typeparam>
        /// <param name="queryFilter">Filter to paginate results</param>
        /// <param name="dataSet">List of data</param>
        /// <returns>Paginator</returns>
        private PageListDTO<T> Paginator<T>(QueryFilterDTO queryFilter, List<T> dataSet)
        {
            //check if ItemPerPage has value, if not set 10 record by default
            queryFilter.ItemPerPage = queryFilter.ItemPerPage == 0 ? 10 : queryFilter.ItemPerPage;

            PageListDTO<T> limitQuery = new();
            int totalRecords = dataSet.Count();
            var countOfPages = totalRecords / queryFilter.ItemPerPage;

            limitQuery.TotalRecords = totalRecords;
            limitQuery.TotalPages = countOfPages;

            //Return all record if user don't put any filter
            if (queryFilter.PageIndex == 0)
            {
                limitQuery.ResultList = dataSet.Take(queryFilter.ItemPerPage).ToList();
                return limitQuery;
            }

            //start and end record 
            var endIndex = queryFilter.ItemPerPage * queryFilter.PageIndex;
            var startIndex = endIndex - queryFilter.ItemPerPage;

            //Extract range of rows
            var range = new Range(startIndex, endIndex);
            limitQuery.ResultList = dataSet.Take(range).ToList();

            return limitQuery;
        }
    }
}
