using System.Linq;
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

        [HttpGet("{cityId}/pointsofinterest/{id}")]
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
    }
}