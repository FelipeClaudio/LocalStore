using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace LocalStore.Infrastructure.Database
{
    public static class ConfigurationUtilities
    {
        public static string GetConnectionStringFromConnectionKey(string connectionKey)
        {
            var rootFolder = GetProjectRootFolder(Directory.GetCurrentDirectory());
            var configuration = new ConfigurationBuilder().AddJsonFile(rootFolder + "\\LocalStore.Application\\appsettings.Development.json").Build();
            return configuration.GetConnectionString(connectionKey); 
        }

        private static string GetProjectRootFolder(string path)
        {
            var mainDir = "LocalStore";
            var mainDirIndex = path.IndexOf(mainDir);
            return path.Remove(mainDirIndex + mainDir.Length);
        }
    }
}
