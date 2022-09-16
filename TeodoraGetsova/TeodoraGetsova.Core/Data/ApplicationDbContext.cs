using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeodoraGetsova.Core.Entities;

namespace TeodoraGetsova.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventBase> Events { get; set; }
        public DbSet<EventImage> Images { get; set; }
        public DbSet<EventType> Types { get; set; }




    }
}