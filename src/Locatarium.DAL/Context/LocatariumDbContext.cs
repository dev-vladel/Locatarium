using Locatarium.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Locatarium.DAL.Context
{
    public class LocatariumDbContext : DbContext
    {
        public LocatariumDbContext(DbContextOptions<LocatariumDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResidenceCategory>().HasKey(rc => new { rc.ResidenceId, rc.CategoryId});
        }

        public DbSet<Address> AddressTable { get; set; }

        public DbSet<Category> CategoryTable { get; set; }

        public DbSet<Rating> RatingTable { get; set; }

        public DbSet<Residence> ResidenceTable { get; set; }

        public DbSet<ResidenceCategory> ResidenceCategoryTable { get; set; }

        public DbSet<User> UserTable { get; set; }


    }
}
