using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SubwayStation.Contracts;
using SubwayStation.Domain.DTOs;
using SubwayStation.Domain.Models;
using SubwayStation.Domain.ViewModels;

namespace SubwayStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubwaysController : ControllerBase
    {
        private readonly ISubwayService _subwayService;
        private readonly IMapper _mapper;

        public SubwaysController(IMapper mapper, ISubwayService subwayService)
        {
            _mapper = mapper;
            _subwayService = subwayService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryFilterDTO queryFilter) 
        { 
            var stations = await _subwayService.GetAllSybways(queryFilter);
            return Ok(stations);
        }

        [HttpGet(nameof(GetFrequently))]
        public async Task<IActionResult> GetFrequently([FromQuery] QueryFilterDTO queryFilter)
        {
            var stations = await _subwayService.GetFrequentlyStations(queryFilter);
            return Ok(stations);
        }

        [HttpPost(nameof(Distances))]
        public async Task<IActionResult> Distances([FromBody] DistancesViewModel coords)
        {
            var distances = await _subwayService.CalcDistances(coords);
            return Ok(new ResponseModel {  Message = distances });
        }

        [HttpPost(nameof(TakeTrainRail))]
        public async Task<IActionResult> TakeTrainRail([FromBody] int subwayId)
        {
            var usedSubway = await _subwayService.SaveFrequentlyStations(subwayId);
            return Ok(new ResponseModel { Message = usedSubway ? "Subway taked" : "Subway full" });
        }

        [HttpPost(nameof(Seed))]
        public async Task<IActionResult> Seed()
        {
            var result = await _subwayService.PopulateTable();
            return Ok(new ResponseModel { Message = result ? "Table seed successfully" : "Tables seeded" });
        }
    }
}
