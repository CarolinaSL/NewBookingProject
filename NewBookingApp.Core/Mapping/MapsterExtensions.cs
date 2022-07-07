﻿using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NewBookingApp.Core.Mapping
{
    public static class MapsterExtensions
    {
        public static IServiceCollection AddCustomMapster(this IServiceCollection services, Assembly assembly)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(assembly);
            var mapperConfig = new Mapper(typeAdapterConfig);
            services.AddSingleton<IMapper>(mapperConfig);

            return services;
        }
    }
}
