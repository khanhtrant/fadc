using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    //[Route("api/[controller])]
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        // [HttpGet("api/cities")]
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object>()
            {
                new { id = 1, Name = "Hanoi" },
                new { id = 2, Name = "TP.HoChiMinh" }
            });
        }
    }
}