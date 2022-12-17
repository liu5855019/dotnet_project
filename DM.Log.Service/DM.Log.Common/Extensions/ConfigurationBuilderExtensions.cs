namespace DM.Log.Common
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.FileProviders;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class ConfigurationBuilderExtensions
    {
        private static string GetEnviroment(string envName = "ASPNETCORE_ENVIRONMENT")
        {
            string env = string.Empty;
            if (!string.IsNullOrWhiteSpace(envName))
            {
                env = Environment.GetEnvironmentVariable(envName);
            }

            return env;
        }

        public static IConfigurationBuilder AddJsonFileAndEnvironmentVariables(this IConfigurationBuilder builder, string jsonFile, bool isOptional = true, bool reloadOnChange = false)
        {
            return AddJsonFileAndEnvironmentVariables(builder, GetEnviroment(), jsonFile, isOptional, reloadOnChange);
        }

        public static IConfigurationBuilder AddJsonFileAndEnvironmentVariables(this IConfigurationBuilder builder, string env, string jsonFile, bool isOptional = true, bool reloadOnChange = false)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (!string.IsNullOrWhiteSpace(jsonFile)
                && ".json".Equals(Path.GetExtension(jsonFile), StringComparison.OrdinalIgnoreCase))
            {
                var sourceFileProvider = builder.GetFileProvider() as PhysicalFileProvider ?? throw new ArgumentNullException();
                var rootName = Path.GetDirectoryName(jsonFile);
                var filePath = Path.Combine(sourceFileProvider.Root, rootName).Trim().Replace('\\', Path.DirectorySeparatorChar);

                using (var fileProvider = new PhysicalFileProvider(filePath))
                {
                    var fileName = Path.GetFileNameWithoutExtension(jsonFile);
                    builder = builder.AddJsonFile(fileProvider, $"{fileName}.json", isOptional, reloadOnChange);

                    if (!string.IsNullOrWhiteSpace(env))
                    {
                        builder = builder.AddJsonFile(fileProvider, $"{fileName}.{env}.json", isOptional, reloadOnChange);
                    }

                    builder = builder.AddEnvironmentVariables();
                }
            }
            return builder;
        }
        private static IEnumerable<string> GetIncludedJsonFiles(IConfigurationRoot configuration, string sectionName)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var section = configuration.GetSection(sectionName);
            if (section?.Exists() == true)
            {
                return section.Get<IEnumerable<string>>();
            }

            return Enumerable.Empty<string>();
        }

        public static IConfigurationBuilder AddIncludedJsonFile(this IConfigurationBuilder builder, string envName = "ASPNETCORE_ENVIRONMENT", string sectionName = "IncludedJsonFiles", bool isOptional = true, bool reloadOnChange = false)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var configRoot = builder.Build();
            var jsonFiles = GetIncludedJsonFiles(configRoot, sectionName);

            if (jsonFiles.Any())
            {
                var environment = GetEnviroment(envName);

                foreach (var jsonFile in jsonFiles)
                {
                    builder = builder.AddJsonFileAndEnvironmentVariables(environment, jsonFile, isOptional, reloadOnChange);
                }
            }

            return builder;
        }

        public static IConfigurationBuilder AddAppsettingsJsonFileAndEnvironmentVariables(this IConfigurationBuilder builder, bool isOptional = true, bool reloadOnChange = false)
        {
            return builder.AddJsonFileAndEnvironmentVariables("appsettings.json", isOptional, reloadOnChange).AddIncludedJsonFile(isOptional: isOptional, reloadOnChange: reloadOnChange);
        }
    }
}
