using AutoMapper;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [Route("api/cities")]
    //[Authorize]
    [Authorize(Policy ="MustBeFromTestCity")]
    [ApiController]
   
    
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        const int MaxPageSize = 20;
        public CitiesController(ICityRepository cityRepository, IMapper mapper)
        {
           _cityRepository = cityRepository;
           _mapper = mapper;
        }

        //https://localhost:{{portNumber}}/api/cities?name=London
        [HttpGet]
        [Route("getcities")]
        public async Task <ActionResult<IEnumerable<CityWithoutPointsDto>>> GetCities()
        {

            var cities =  await _cityRepository.GetAllCitiesAsync();

            //with AutoMapper
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsDto>>(cities));

            //without AutoMapper
            //var result = new List<CityWithoutPointsDto>();
            //foreach (var city in cities)
            //{
            //    result.Add(new CityWithoutPointsDto
            //    {
            //        Id = city.Id,
            //        Name = city.Name,
            //        Description = city.Description ?? string.Empty,
            //    });
            //}
            //return Ok(result);

        }

        [HttpGet("{id}")]
        //return ActionResult, because there are two possible types to return
        public async Task<ActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = await _cityRepository.GetCityAsync(id, false);

            if (city == null)
            {
                return NotFound();
            }
            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }
            return Ok(_mapper.Map<CityWithoutPointsDto>(city));
        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsDto>>> GetCitiesByPage(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }
            var result = await _cityRepository.GetAllCitiesAsync(name, searchQuery, pageNumber, pageSize);

            return Ok(_mapper.Map<CityWithoutPointsDto>(result));
        }
    }
}
