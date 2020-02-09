using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofintrest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _log;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, IMailService mailService, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetPointsOfIntrest(int cityId)
        {
            try
            {
                //throw new Exception("Test Exception");
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _log.LogInformation($"City with id {cityId} was not found when accessing points of intrest.");
                    return NotFound();
                }
                var pointsOfIntrestForCity = _cityInfoRepository.GetPointsOfIntrestForCity(cityId);
                //var pointsOfIntrestForCityResults = new List<PointOfInterestDto>();
                //foreach (var poi in pointsOfIntrestForCity)
                //{
                //    pointsOfIntrestForCityResults.Add(new PointOfInterestDto()
                //    {
                //        Id = poi.Id,
                //        Name = poi.Name,
                //        Description = poi.Description
                //    });
                //}
                return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfIntrestForCity));
            }
            catch (Exception ex)
            {
                _log.LogCritical($"An exception occured when getting points of intrest for city with id {cityId}", ex);
                return StatusCode(500, "Something went wrong processing your request");
            }
        }

        [HttpGet("{id}", Name = "GetPointOfIntrest")]
        public IActionResult GetPointOfIntrest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            var pointOfIntrest = _cityInfoRepository.GetPointOfIntrestForCity(cityId, id);
            if(pointOfIntrest == null)
            {
                return NotFound();
            }
            //var pointOfIntrestResult = new PointOfInterestDto()
            //{
            //    Id = pointOfIntrest.Id,
            //    Name = pointOfIntrest.Name,
            //    Description = pointOfIntrest.Description
            //};
            return Ok(_mapper.Map<PointOfInterestDto>(pointOfIntrest));
        }

        [HttpPost]
        public IActionResult CreatePointOfIntrest(int cityId, [FromBody]PointOfIntrestForCreationDto pointOfIntrest)
        {
            if(pointOfIntrest.Description == pointOfIntrest.Name)
            {
                ModelState.AddModelError("description", "The description and name can not have the same value");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_cityInfoRepository.CityExists(cityId))
                return NotFound();

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            //DEMO purposes this will be improved on
            var maxPointOfIntrest = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfIntrest).Max(p => p.Id);
            var finalPointOfIntrest = new PointOfInterestDto()
            {
                //Id = ++maxPointOfIntrest,
                Name = pointOfIntrest.Name,
                Description = pointOfIntrest.Description
            };

            city.PointsOfIntrest.Add(finalPointOfIntrest);
            return CreatedAtRoute("GetPointOfIntrest", new { cityId, id = finalPointOfIntrest.Id }, finalPointOfIntrest );
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfIntrest(int cityId, int id, [FromBody]PointOfIntrestForUpdateDto pointOfIntrest)
        {
            if (pointOfIntrest.Description == pointOfIntrest.Name)
            {
                ModelState.AddModelError("description", "The description and name can not have the same value");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var pointOfIntrestFromStore = city.PointsOfIntrest.FirstOrDefault(p => p.Id == id);
            if (pointOfIntrestFromStore == null)
                return NotFound();
            pointOfIntrestFromStore.Name = pointOfIntrest.Name;
            pointOfIntrestFromStore.Description = pointOfIntrest.Description;
            return NoContent();
        }

        [HttpPatch("{id}")] //Json Patch (RFC 6902)
        public IActionResult PartiallyUpdatePointOfIntrest(int cityId, int id, [FromBody]JsonPatchDocument<PointOfIntrestForUpdateDto> patchDoc)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var pointOfIntrestFromStore = city.PointsOfIntrest.FirstOrDefault(p => p.Id == id);
            if (pointOfIntrestFromStore == null)
                return NotFound();

            var pointOfIntrestToPatch = new PointOfIntrestForUpdateDto()
            {
                Name = pointOfIntrestFromStore.Name,
                Description = pointOfIntrestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfIntrestToPatch, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pointOfIntrestToPatch.Name == pointOfIntrestToPatch.Description)
                ModelState.AddModelError("Description", "The title and description can not have the same value");
            if (!TryValidateModel(pointOfIntrestToPatch))
                return BadRequest(ModelState);

            pointOfIntrestFromStore.Name = pointOfIntrestToPatch.Name;
            pointOfIntrestFromStore.Description = pointOfIntrestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointOfIntrest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var pointOfIntrestFromStore = city.PointsOfIntrest.FirstOrDefault(c => c.Id == id);
            if (pointOfIntrestFromStore == null)
                return NotFound();

            city.PointsOfIntrest.Remove(pointOfIntrestFromStore);

            _mailService.Send($"Point of Intrest DELETED", 
                $"The Point Of Intrest {pointOfIntrestFromStore.Name} with id of {pointOfIntrestFromStore.Id} has been deleted from {city.Name}");

            return NoContent();
        }
    }
}
