using AutoMapper;
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
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            //var results = new List<CityWithoutPointsOfIntrestDto>();
            //foreach (var cityEntity in cityEntities)
            //{
            //    results.Add(new CityWithoutPointsOfIntrestDto
            //    {
            //        Id = cityEntity.Id,
            //        Name = cityEntity.Name,
            //        Description = cityEntity.Description
            //    }); ;
            //}

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfIntrestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfIntrest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfIntrest);
            if (city == null)
                return NotFound();

            if (includePointsOfIntrest)
            {
                //var cityResult = _mapper.Map<CityDto>(city);
                //var cityResult = new CityDto()
                //{
                //    Id = city.Id,
                //    Name = city.Name,
                //    Description = city.Description
                //};
                //foreach (var poi in city.PointsOfIntrest)
                //{
                //    cityResult.PointsOfIntrest.Add(new PointOfInterestDto()
                //    {
                //        Id = poi.Id,
                //        Name = poi.Name,
                //        Description = poi.Description
                //    });
                //}
                return Ok(_mapper.Map<CityDto>(city));
            }
            //var cityWithoutPointsOfIntrest = _mapper.Map<CityWithoutPointsOfIntrestDto>(city);
            //var cityWithoutPointsOfIntrest = new CityWithoutPointsOfIntrestDto()
            //{
            //    Id = city.Id,
            //    Name = city.Name,
            //    Description = city.Description
            //};
            //var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            
            return Ok(_mapper.Map<CityWithoutPointsOfIntrestDto>(city));
        }
    }
}
