using API.Helpers;
using API.IHelpers;
using Microsoft.Extensions.DependencyInjection;
using Core.UnitOfWork;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IJWTManager, JWTManager>();
            return services;
        }
    }
}
