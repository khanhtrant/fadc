using System.Collections.Generic;
using System.Linq;
using FirstAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityDbContext _context;

        public CityInfoRepository(CityDbContext context)
        {
            _context = context;
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public bool CityExists(int CityId)
        {
            return _context.City.Any(c => c.Id == CityId);
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }

        public IEnumerable<City> GetCities()
        {
            return _context.City.OrderBy(m => m.Name).ToList();
        }

        public City GetCity(int CityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _context.City.Include(c => c.PointsOfInterest)
                .Where(c => c.Id == CityId)
                .FirstOrDefault();
            }
            return _context.City.Where(c => c.Id == CityId)
                            .FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterestForCity(int CityId, int Id)
        {
            return _context.PointsOfInterest
                .Where(p => p.CityId == CityId && p.Id == Id)
                .FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int CityId)
        {
            return _context.PointsOfInterest
                        .Where(p => p.CityId == CityId)
                        .ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}