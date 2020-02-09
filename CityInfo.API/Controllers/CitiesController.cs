using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            var results = new List<CityWithoutPointsOfIntrestDto>();
            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfIntrestDto
                {
                    Id = cityEntity.Id,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description
                }); ;
            }
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfIntrest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfIntrest);
            if (city == null)
                return NotFound();

            if (includePointsOfIntrest)
            {
                var cityResult = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };
                foreach (var poi in city.PointsOfIntrest)
                {
                    cityResult.PointsOfIntrest.Add(new PointOfInterestDto()
                    {
                        Id = poi.Id,
                        Name = poi.Name,
                        Description = poi.Description
                    });
                }
                return Ok(cityResult);
            }

            var cityWithoutPointsOfIntrest = new CityWithoutPointsOfIntrestDto()
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description
            };
            //var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            
            return Ok(cityWithoutPointsOfIntrest);
        }
    }
}
