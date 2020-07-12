using API.Helpers;
using API.IHelpers;
using Microsoft.Extensions.DependencyInjection;
using Core.UnitOfWork;
using Core.IRepository;
using Core.Repository;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IJWTManager, JWTManager>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
