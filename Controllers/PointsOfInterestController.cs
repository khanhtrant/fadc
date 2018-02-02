using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FirstAPI.Models;
using FirstAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FirstAPI.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with {cityId} doesn't exist");
                    return NotFound();
                }
                var city = _cityInfoRepository.GetPointsOfInterestForCity(cityId);
                var result = Mapper.Map<IEnumerable<PointOfInterestDto>>(city);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Exception: {cityId}", e);
                return StatusCode(500, "Problem");
            }
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                _logger.LogInformation($"City with {cityId} doesn't exist");
                return NotFound();
            }
            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<PointOfInterestDto>(pointOfInterest);
            return Ok(result);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody]PointOfInterestForCreation pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Desciption", "Name does not equal description");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Problem");
            }

            var createPointOfInterestToReturn = Mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                id = createPointOfInterestToReturn.Id,
            }, createPointOfInterestToReturn);
        }

        [HttpPut("{cityId}/pointsofinterest/{Id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int Id, [FromBody]PointOfInterestToUpdate pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Desciption", "Name does not equal description");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, Id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(pointOfInterest, pointOfInterestEntity);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Problem");
            }

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestToUpdate> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = Mapper.Map<PointOfInterestToUpdate>(pointOfInterestEntity);

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500,"Problem");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.SingleOrDefault(m => m.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var model = city.PointsOfInterest.SingleOrDefault(m => m.Id == id);
            if (model == null)
            {
                return BadRequest();
            }

            city.PointsOfInterest.Remove(model);
            return NoContent();
        }
    }
}
