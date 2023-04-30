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
        /// Define the name of your publisher/developper. This is used for default paths. If set to null, not used
        /// </summary>
        public static string MyPublisherName { get; set; } = null;

        /// <summary>
        /// Define the name of your application. This is used for default paths
        /// </summary>
        public static string MyAppName { get; set; } = null;

        /// <summary>
        /// The type of backup
        /// </summary>
        public enum BackupMode
        {
            /// <summary>
            /// No backup of the file
            /// </summary>
            None,

            /// <summary>
            /// Add a .bak to the same path
            /// </summary>
            dotBak,

            /// <summary>
            /// Add a datetime format and store to the default path in appdata. Get the path with GetDefaultBackupPath
            /// </summary>
            datetimeFormatAppdata,
        }

        /// <summary>
        /// Get the default path to the settings file if not specified. The format is : %appdata%/Roaming/<see cref="MyPublisherName"/>/<see cref="MyAppName"/>/Settings/config.txt (or .zip).
        /// If <see cref="MyPublisherName"/> is set to null, it is not used in the path.
        /// <see cref="MyAppName"/> MUST be set otherwise an exception will be thrown
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetDefaultSettingFilePath(bool isZip)
        {
            if (string.IsNullOrEmpty(MyAppName))
                throw new ArgumentNullException("The app name is null...", "MyAppName");

            // %Appdata%\<publisher>\<appname>\settings

            if (string.IsNullOrEmpty(MyPublisherName))
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    MyAppName,
                    "Settings",
                    "config" + (isZip ? ".zip" : ".txt")
                );
            else
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    MyPublisherName,
                    MyAppName,
                    "Settings",
                    "config" + (isZip ? ".zip" : ".txt")
                );
        }

        /// <summary>
        /// Get the default path to save settings if not specified. The format is : %appdata%/Roaming/<see cref="MyPublisherName"/>/<see cref="MyAppName"/>/Settings/config.txt (or .zip).
        /// If <see cref="MyPublisherName"/> is set to null, it is not used in the path.
        /// <see cref="MyAppName"/> MUST be set otherwise an exception will be thrown
        /// </summary>
        public static string GetDefaultBackupPath()
        {
            if (string.IsNullOrEmpty(MyAppName))
                throw new ArgumentNullException("The app name is null...", "MyAppName");

            // %Appdata%\ESN\Backups\
            if (string.IsNullOrEmpty(MyPublisherName))
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    MyAppName,
                    "Backups"
                );
            else
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    MyPublisherName,
                    MyAppName,
                    "Backups"
                );
        }

        /// <summary>
        /// Backup the previous file
        /// </summary>
        private static void BackupSetting(string settingPath, bool hide, BackupMode mode)
        {
            if (mode == BackupMode.None)
                return;

            // If current setting doesn't exists, abort, nothing to backup...
            if (!File.Exists(settingPath))
                return;

            string bakPath = null;
            switch (mode)
            {
                case BackupMode.dotBak:
                    // Append .bak
                    bakPath = settingPath + ".bak";
                    // This method overwrite the file over and over. Delete before
                    if (File.Exists(bakPath))
                        File.Delete(bakPath);
                    break;
                case BackupMode.datetimeFormatAppdata:
                    // Get filename, append datetime and move to appdata
                    string appDataPath = GetDefaultBackupPath();
                    string fileName = Path.GetFileNameWithoutExtension(settingPath);
                    string ext = Path.GetExtension(settingPath);
                    bakPath = Path.Combine(
                        appDataPath,
                        $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}{ext}"
                    );
                    // Check directory
                    if (!Directory.Exists(Path.GetDirectoryName(bakPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(bakPath));
                    }
                    break;
                default:
                    break;
            }

            // Move the current setting
            if (!File.Exists(bakPath)) // Too recent change
                File.Move(settingPath, bakPath);

            if (hide)
            {
                File.SetAttributes(bakPath, FileAttributes.Hidden);
            }
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        /// <param name="internalFileName">Name inside the zip file. No actual effect on the proces, just to make it nicer</param>
        public static void SaveTo<T>(
            string path,
            T setting,
            BackupMode backup = BackupMode.None,
            bool indent = true,
            bool hide = false,
            bool zipFile = true,
            string internalFileName = "settings.txt"
        )
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            string dirPath = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dirPath))
                throw new ArgumentException("Invalid path", nameof(path));
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            if (backup != BackupMode.None)
            {
                BackupSetting(path, hide, backup);
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
                    zip.CreateEntryFromFile(tempFile, internalFileName);
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
            T setting,
            BackupMode backup = BackupMode.None,
            bool indent = true,
            bool hide = false,
            bool zipFile = true
        )
        {
            SaveTo(GetDefaultSettingFilePath(zipFile), setting, backup, indent, hide, zipFile);
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public static bool LoadFrom<T>(string path, out T output, bool zipFile = true)
        {
            if (File.Exists(path))
            {
                string fileData = null;
                if (zipFile)
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
        public static bool LoadFromDefault<T>(out T output, bool zipFile = true)
        {
            return LoadFrom(GetDefaultSettingFilePath(zipFile), out output, zipFile);
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
