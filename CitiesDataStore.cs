using System.Collections.Generic;
using FirstAPI.Models;

namespace FirstAPI
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id=1,
                    Name="Paris",
                    Description="The big iron tower in the middle"
                },
                new CityDto()
                {
                    Id=2,
                    Name="Los Angeles",
                    Description="The yellow long bridge"
                }
            };
        }
    }
}