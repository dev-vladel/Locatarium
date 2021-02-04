using Locatarium.BLL.BusinessLogic;
using Locatarium.BLL.IBusinessLogic;
using Microsoft.Extensions.DependencyInjection;

namespace Locatarium.BLL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLocatariumBLLService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserBLL, UserBLL>();
            serviceCollection.AddScoped<IResidenceBLL, ResidenceBLL>();
            serviceCollection.AddScoped<ICategoryBLL, CategoryBLL>();
            serviceCollection.AddScoped<IRatingBLL, RatingBLL>();

            return serviceCollection;
        }
    }
}