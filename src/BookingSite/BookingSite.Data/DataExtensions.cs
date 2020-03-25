using System;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSite.Data
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            //configure your Data Layer services here
            return services;
        }
    }
}