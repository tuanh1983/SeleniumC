using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Automation.Utils
{
    public static class ConfigReader
    {
        private static IConfigurationRoot configuration;

        static ConfigReader()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static T GetProductModuleConfig<T>(string product, string module) where T : new()
        {
            var section = configuration.GetSection($"Products").GetChildren()
                .FirstOrDefault(p => p["Product"] == product)?
                .GetSection("Modules").GetSection(module);

            if (section == null) throw new Exception($"Configuration not found for Product: {product}, Module: {module}");

            var result = new T();
            section.Bind(result);
            return result;
        }
    }
}
