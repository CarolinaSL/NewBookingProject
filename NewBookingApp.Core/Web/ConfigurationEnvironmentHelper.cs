using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Core.Web
{
    public static class ConfigurationEnvironmentHelper
    {
        public static IConfiguration GetConfiguration(string basePath = null)
        {
            basePath ??= Directory.GetCurrentDirectory();
            var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentVariable}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
