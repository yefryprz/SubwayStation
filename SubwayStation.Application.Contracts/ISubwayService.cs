using SubwayStation.Domain.DTOs;
using SubwayStation.Domain.ViewModels;

namespace SubwayStation.Contracts
{
    public interface ISubwayService
    {
        Task<PageListDTO<SubwayDTO>> GetAllSybways(QueryFilterDTO queryFilter);
        Task<PageListDTO<FrequentlyDTO>> GetFrequentlyStations(QueryFilterDTO queryFilter);
        Task<bool> SaveFrequentlyStations(int subwayId);
        Task<string> CalcDistances(DistancesViewModel distances);
        Task<bool> PopulateTable();
    }
}
