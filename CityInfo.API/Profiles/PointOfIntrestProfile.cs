using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Profiles
{
    public class PointOfIntrestProfile : Profile
    {
        public PointOfIntrestProfile()
        {
            CreateMap<Entities.PointOfIntrest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfIntrestForCreationDto, Entities.PointOfIntrest>();
        }
    }
}
