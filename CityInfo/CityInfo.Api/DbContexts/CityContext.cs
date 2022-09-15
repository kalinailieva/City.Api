using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.DbContexts
{
    public class CityContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityContext(DbContextOptions<CityContext> options) :base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("London")
                {
                    Id = 1,
                    Description = "Beautiful and nice"
                },
                new City("Paris")
                {
                    Id = 2,
                    Description = "Beautiful and lovely"
                });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Tower")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "The best point ever"
                });

            base.OnModelCreating(modelBuilder);

        }
    }
}
