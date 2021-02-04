using Locatarium.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Locatarium.Designer
{
    public class LocatariumDbContextFactory : IDesignTimeDbContextFactory<LocatariumDbContext>
    {
        public LocatariumDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LocatariumDbContext>();

            builder.UseSqlServer("Server=server;Database=database;User=user;Password=password;Trusted_Connection=False;Encrypt=True;Connection Timeout=60;TrustServerCertificate=True", x => x.MigrationsAssembly("Locatarium.Designer"));

            return new LocatariumDbContext(builder.Options);
        }
    }
}
