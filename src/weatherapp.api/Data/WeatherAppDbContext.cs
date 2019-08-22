using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain;

namespace WeatherApp.Data
{
    public class WeatherAppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<CityForecast> CityForecasts { get; set; }

        public WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Seed();
           //I'm commenting this out as seeding takes too long initially so I deleted the seed file
        }
    }
}
