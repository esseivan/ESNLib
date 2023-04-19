using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    /// <summary>
    /// Manage settings for an application that are serialized into a json file (non-secure !)
    /// </summary>
    public abstract class SettingsManager
    {
        /// <summary>
        /// Get the default path to save settings if not specified
        /// </summary>
        public static string GetDefaultPath(string appName, bool isZip = true)
        {
            // %Appdata%\ESN\<appName>
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ESN",
                "Defaults",
                appName + "_config" + (isZip ? ".zip" : ".txt")
            );
        }

        /// <summary>
        /// Backup the previous file
        /// </summary>
        private static void BackupSetting(string path, bool hide)
        {
            // If current setting doesn't exists, abort. Do not delete the previous backup
            if (!File.Exists(path))
                return;

            // Delete the previous backup and replace it with the current setting
            string bakPath = path + ".bak";
            if (File.Exists(bakPath))
                File.Delete(bakPath);
            File.Move(path, bakPath);
            if (hide)
            {
                File.SetAttributes(bakPath, FileAttributes.Hidden);
            }
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public static void SaveTo<T>(
            string path,
            T setting,
            bool backup = true,
            bool indent = true,
            bool hide = false,
            bool zipFile = true
        )
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            string dirPath = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dirPath))
                throw new ArgumentException("Invalid path", nameof(path));
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            if (backup)
            {
                BackupSetting(path, hide);
            }

            string content = Serialize(setting, indent);

            if (zipFile)
            {
                string tempFile = Path.GetTempFileName();
                File.WriteAllText(tempFile, content);

                if (File.Exists(path))
                    File.Delete(path);

                using (ZipArchive zip = ZipFile.Open(path, ZipArchiveMode.Create))
                {
                    zip.CreateEntryFromFile(tempFile, "settings.txt");
                }

                File.Delete(tempFile);
            }
            else
            {
                if (File.Exists(path))
                    File.SetAttributes(path, FileAttributes.Normal); // Must be unhidden in order to write to it
                File.WriteAllText(path, content);
            }

            if (hide)
            {
                File.SetAttributes(path, FileAttributes.Hidden);
            }
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public static void SaveToDefault<T>(
            string appName,
            T setting,
            bool backup = true,
            bool indent = true,
            bool hide = false,
            bool zipFile = true
        )
        {
            SaveTo(GetDefaultPath(appName), setting, backup, indent, hide, zipFile);
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public static bool LoadFrom<T>(string path, out T output, bool isZipped = true)
        {
            if (File.Exists(path))
            {
                string fileData = null;
                if (isZipped)
                {
                    using (ZipArchive zip = ZipFile.Open(path, ZipArchiveMode.Read))
                    using (Stream st = zip.Entries[0].Open())
                    using (StreamReader sw = new StreamReader(st))
                    {
                        fileData = sw.ReadToEnd();
                    }
                }
                else
                {
                    fileData = File.ReadAllText(path);
                }

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
        public static bool LoadFromDefault<T>(string appName, out T output, bool isZipped = true)
        {
            return LoadFrom(GetDefaultPath(appName), out output, isZipped);
        }

        /// <summary>
        /// deserialize data
        /// </summary>
        private static T Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }

        /// <summary>
        /// serialize data
        /// </summary>
        private static string Serialize<T>(T data, bool indent)
        {
            JsonSerializerOptions jso = new JsonSerializerOptions { WriteIndented = indent };
            return JsonSerializer.Serialize(data, jso);
        }
    }
}
