using System.Collections.Generic;
using FirstAPI.Entities;

namespace FirstAPI.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int CityId,bool includePointsOfInterest);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int CityId);
        PointOfInterest GetPointOfInterestForCity(int CityId, int Id);
        bool CityExists(int CityId);
        void AddPointOfInterestForCity(int cityId,PointOfInterest pointOfInterest);
        bool Save();
    }
}