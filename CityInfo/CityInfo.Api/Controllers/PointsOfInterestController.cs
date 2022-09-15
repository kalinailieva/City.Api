using AutoMapper;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            ICityRepository cityRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(int cityId)
        {
            var isCityExist = await _cityRepository.CityExistsAsync(cityId);

            var pointsOfInterest = await _cityRepository.GetPointsOfInterestAsync(cityId).ConfigureAwait(false);

            if (!isCityExist)
            {
                _logger.LogInformation("City wasn't found");
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest));

        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task <ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(int cityId, int pointOfInterestId )
        {
            var isCityExist = await _cityRepository.CityExistsAsync(cityId);

            if (!isCityExist)
            {
                return NotFound();
            }

            var pointOfInterest = await _cityRepository.GetPointOfInterestAsync(cityId, pointOfInterestId).ConfigureAwait(false);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> Create(int cityId, PointOfInterestCreateDto model)
        {
            if (!await _cityRepository.CityExistsAsync(cityId).ConfigureAwait(false))
            {
                return NotFound();
            }

            var newPoint = _mapper.Map<Entities.PointOfInterest>(model);
            await _cityRepository.AddPointOfInterestAsync(cityId, newPoint);
            await _cityRepository.SaveChangesAsync();


            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = newPoint.Id,
                },
                newPoint);
        }

        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> Update (int cityId, int pointOfInterestId, PointOfInterestUpdateDto model)
        {
            if (!await _cityRepository.CityExistsAsync(cityId).ConfigureAwait(false))
            {
                return NotFound();
            }
            var currentEntity = _cityRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if (currentEntity == null)
            {
                return NotFound();
            }

            //auto replace the one with another. Special Case! it is track by the db context also
            await _mapper.Map(model, currentEntity);
            await _cityRepository.SaveChangesAsync();
             return NoContent();

        }

        [HttpDelete("{pointofinterestid}")]
        public async Task<ActionResult> Delete(int cityId, int pointOfInterestId)
        {
            if(!await _cityRepository.CityExistsAsync(cityId).ConfigureAwait(false))
            {
                return NotFound();
            }

            var pointOfInterest = await _cityRepository.GetPointOfInterestAsync(cityId, pointOfInterestId).ConfigureAwait(false);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            await _cityRepository.DeletePointOfInterestAsync(cityId, pointOfInterest);
            _mailService.Send("Point of interest deleted", $"Point of interest with id {pointOfInterestId} was deleted");

           return NoContent();//DELETE doesn't return anything in this case

        }
    }
}
