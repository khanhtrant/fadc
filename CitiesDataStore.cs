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
                    Description="The big iron tower in the middle",
                    PointsOfInterest=new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=1,
                            Name="Effel Tower",
                            Description="Old Iron Tower in the middle of city"
                        },
                        new PointOfInterestDto()
                        {
                            Id=2,
                            Name="Seine River",
                            Description="The Seine (/seɪn/ SAYN; French: La Seine, pronounced [la sɛːn]) is a 777-kilometre-long (483 mi) river and an important commercial waterway within the Paris Basin in the north of France."
                        }
                    }
                },
                new CityDto()
                {
                    Id=2,
                    Name="Los Angeles",
                    Description="The yellow long bridge",
                    PointsOfInterest=new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=1,
                            Name="Golden Bridge",
                            Description="Long Nice Bridge"
                        }
                    }
                }
            };
        }
    }
}