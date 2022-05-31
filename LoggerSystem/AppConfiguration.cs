using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace LoggerSystem
{
    public static class AppConfiguration
    {
        public static readonly string _connectionString = string.Empty;
        static AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("LoggerConnectionStrings").GetSection("LoggerConnection").Value;
        }
        public static string ConnectionString
        {
            get => _connectionString;
        }
    }
}

