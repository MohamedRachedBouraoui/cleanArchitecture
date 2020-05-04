using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Uccspu.Affaires.Communs.Comportements;

namespace Uccspu.Affaires
{
    public static class GestionDeDependances
    {
        public static IServiceCollection AjouterAffaires(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ComportementExceptionNonGeree<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ComportementAuthentification<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ComportementValidation<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ComportementPerformance<,>));

            return services;
        }
    }
}
