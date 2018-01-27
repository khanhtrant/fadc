using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    //[Route("api/[controller])]
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var model = CitiesDataStore.Current.Cities.FirstOrDefault(m => m.Id == id);
            if (model==null)
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}