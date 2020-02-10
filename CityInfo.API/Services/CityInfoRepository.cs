using CityInfo.API.Contexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointsOfIntrest)
        {
            if (includePointsOfIntrest)
            {
                return _context.Cities.Include(c => c.PointsOfIntrest).Where(c => c.Id == cityId).FirstOrDefault(); 
            }
            return _context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }

        public PointOfIntrest GetPointOfIntrestForCity(int cityId, int pointOfIntrestId)
        {
            return _context.PointOfIntrest.Where(p => p.CityId == cityId && p.Id == pointOfIntrestId).FirstOrDefault();
        }

        public IEnumerable<PointOfIntrest> GetPointsOfIntrestForCity(int cityId)
        {
            return _context.PointOfIntrest.Where(p => p.CityId == cityId).ToList();
        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public void AddPointOfIntrestForCity(int cityId, PointOfIntrest pointOfIntrest)
        {
            var city = GetCity(cityId, includePointsOfIntrest: false);
            city.PointsOfIntrest.Add(pointOfIntrest);
        }

        public void UpdatePointOfIntrestForCity(int cityId, PointOfIntrest pointOfIntrest)
        {
            //TODO add code if needed (Not using Entity FrameWork)
        }

        public void DeletePointOfIntrest(PointOfIntrest pointOfIntrest)
        {
            _context.PointOfIntrest.Remove(pointOfIntrest);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
