using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain;

namespace WeatherApp.Data
{
    public class WeatherAppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
