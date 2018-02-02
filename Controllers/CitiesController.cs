using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
            var cityEntities = _cityInFoRepository.GetCities();
            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int Id, bool includePointsOfInterest = false)
        {
            var city = _cityInFoRepository.GetCity(Id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var CityResult1 = Mapper.Map<CityDto>(city);
                return Ok(CityResult1);
            }

            var CityResult2 = Mapper.Map<CityWithoutPointsOfInterestDto>(city);
            return Ok(CityResult2);
        }
    }
}