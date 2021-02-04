using Locatarium.DAL.Context;
using Locatarium.DAL.DAO;
using Locatarium.DAL.IDAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Locatarium.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLocatariumDALService(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<LocatariumDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            serviceCollection.AddScoped<IUserDAO, UserDAO>();
            serviceCollection.AddScoped<IResidenceDAO, ResidenceDAO>();
            serviceCollection.AddScoped<ICategoryDAO, CategoryDAO>();
            serviceCollection.AddScoped<IRatingDAO, RatingDAO>();

            return serviceCollection;
        }
    }
}