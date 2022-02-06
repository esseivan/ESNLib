using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    public class SettingsManager
    {
        /// <summary>
        /// Get the default path to save settings if not specified
        /// </summary>
        public static string GetDefaultPath(string appName)
        {
            // %Appdata%\ESN\<appName>
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ESN",
                "Defaults",
                appName + "_config.txt");
        }

        /// <summary>
        /// Backup the previous file
        /// </summary>
        private static void BackupSetting(string path)
        {
            // If current setting doesn't exists, abort. Do not delete the previous backup
            if (!File.Exists(path))
                return;

            // Delete the previous backup and replace it with the current setting
            string bakPath = path + ".bak";
            if (File.Exists(bakPath))
                File.Delete(bakPath);
            File.Move(path, bakPath);
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public static void SaveTo<T>(string path, T setting, bool backup = true, bool indent = true)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            string dirPath = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dirPath))
                throw new ArgumentException("Invalid path", nameof(path));
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            if (backup)
                BackupSetting(path);

            File.WriteAllText(path, Serialize(setting, indent));
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public static void SaveToDefault<T>(string appName, T setting, bool backup = true, bool indent = true)
        {
            SaveTo(GetDefaultPath(appName), setting, backup, indent);
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public static bool LoadFrom<T>(string path, out T output)
        {
            if (File.Exists(path))
            {
                // Load settings from raw data
                string fileData = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(fileData))
                {
                    T setting = Deserialize<T>(fileData);
                    output = setting;
                    return true;
                }
            }
            // The previous if statement should return true if completed correctly !!

            output = default;
            return false;
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public static bool LoadFromDefault<T>(string appName, out T output)
        {
            return LoadFrom(GetDefaultPath(appName), out output);
        }

        /// <summary>
        /// deserialize data
        /// </summary>
        private static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// serialize data
        /// </summary>
        private static string Serialize<T>(T data, bool indent)
        {
            return JsonConvert.SerializeObject(data, indent ? Formatting.Indented : Formatting.None);
        }
    }
}
