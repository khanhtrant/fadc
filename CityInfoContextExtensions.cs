using System.Collections.Generic;
using System.Linq;
using FirstAPI.Entities;
using FirstAPI.Models;

namespace FirstAPI
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDatabaseForContext(this CityDbContext context)
        {
            if (context.City.Any())
            {
                return;
            }
            var Cities = new List<City>()
            {
                new City()
                {
                    Name="Paris",
                    Description="The big iron tower in the middle",
                    PointsOfInterest=new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Effel Tower",
                            Description="Old Iron Tower in the middle of city"
                        },
                        new PointOfInterest()
                        {
                            Name="Seine River",
                            Description="The Seine (/seɪn/ SAYN; French: La Seine, pronounced [la sɛːn]) is a 777-kilometre-long (483 mi) river and an important commercial waterway within the Paris Basin in the north of France."
                        }
                    }
                },
                new City()
                {
                    Name="Los Angeles",
                    Description="The yellow long bridge",
                    PointsOfInterest=new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Golden Bridge",
                            Description="Long Nice Bridge"
                        }
                    }
                }
            };
            context.City.AddRange(Cities);
            context.SaveChanges();
        }
    }
}