using System;
using Microsoft.Extensions.DependencyInjection;
using BookingSite.Data;

namespace BookingSite.Domain.Logic
{
    public static class DomainLogicExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddDataServices();
            //configure your Domain Logic Layer services here
            return services;
        }
    }
}