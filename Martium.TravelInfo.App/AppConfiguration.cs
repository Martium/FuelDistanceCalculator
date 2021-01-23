using System.IO;
using System.Reflection;

namespace Martium.TravelInfo.App
{
    public static class AppConfiguration
    {
        private static readonly string DatabaseName = "TravelInfo";

        public static string DatabaseFolder => $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Database";
        public static string DatabaseFile => $"{DatabaseFolder}\\{DatabaseName}.db";
        public static string ConnectionString => $"Data Source={DatabaseFile};Version=3;UseUTF16Encoding=True;";
        public static string TableName => "TravelInfoSettings";
        public static string AppUuid = "e69b2537-3f00-4eaa-adb1-d22b0939667b";
    }
}
