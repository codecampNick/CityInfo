﻿using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointsOfIntrest);
        IEnumerable<PointOfIntrest> GetPointsOfIntrestForCity(int cityId);
        PointOfIntrest GetPointOfIntrestForCity(int cityId, int pointOfIntrestId);
    }
}
