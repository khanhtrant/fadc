using System.Collections.Generic;
using System.Linq;
using FirstAPI.Models;
using FirstAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    //[Route("api/[controller])]
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInFoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInFoRepository = cityInfoRepository;
        }
        [HttpGet()]
        public IActionResult GetCities()
        {
            // return Ok(CitiesDataStore.Current.Cities);
            var cityEntities = _cityInFoRepository.GetCities();
            var result = new List<CityWithoutPointsOfInterestDto>();
            foreach (var city in cityEntities)
            {
                result.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description,
                });
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int Id, bool includePointsOfInterest = false)
        {
            // var model = CitiesDataStore.Current.Cities.FirstOrDefault(m => m.Id == id);
            // if (model==null)
            // {
            //     return NotFound();
            // }
            // return Ok(model);

            var city = _cityInFoRepository.GetCity(Id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var result1 = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };

                foreach (var item in city.PointsOfInterest)
                {
                    result1.PointOfInterest.Add(new PointOfInterestDto()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }

                return Ok(result1);
            }
            var result2 = new CityWithoutPointsOfInterestDto()
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description
            };
            return Ok(result2);
        }
    }
}