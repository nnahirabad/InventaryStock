
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using InventarioComercio.Application.Helpers;
using MediatR;
using FluentValidation;
using InventarioComercio.Application.Behaviors;

namespace InventarioComercio.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(ApplicationServiceRegistration).Assembly );
            services.AddScoped<JwtTokenGenerator>();

            return services;
        }
    }
}