using System.Linq;
using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var model = CitiesDataStore.Current.Cities.SingleOrDefault(m => m.Id == cityId);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model.PointOfInterest);
        }

        [HttpGet("{cityId}/pointsofinterest/{id}",Name="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.SingleOrDefault(m => m.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var PointOfInterest = city.PointOfInterest.SingleOrDefault(m => m.Id == id);
            if (PointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(PointOfInterest);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody]PointOfInterestForCreation pointOfInterest)
        {
            if (pointOfInterest==null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description==pointOfInterest.Name)
            {
                ModelState.AddModelError("Desciption","Name does not equal description");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.SingleOrDefault(m => m.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId=CitiesDataStore.Current.Cities.SelectMany(c=>c.PointOfInterest).Max(p=>p.Id);

            var finalPointOfInterest=new PointOfInterestDto()
            {
                Id=++maxPointOfInterestId,
                Name=pointOfInterest.Name,
                Description=pointOfInterest.Description
            };

            return CreatedAtRoute("GetPointOfInterest",new{
                cityId=cityId,
                id=finalPointOfInterest,
            },finalPointOfInterest);

        }
    }
}
