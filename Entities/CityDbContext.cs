using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Entities
{
    public class CityDbContext:DbContext
    {
        public CityDbContext(DbContextOptions<CityDbContext>options):base(options)
        {
            
        }

        public DbSet<City> City { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
    }
}