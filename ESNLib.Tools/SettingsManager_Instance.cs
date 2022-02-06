using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    [Obsolete("Use SettingsManager instead")]
    public class SettingsManager_Instance<T>
    {
        /// <summary>
        /// The setting to save
        /// </summary>
        private T setting = default;

        /// <summary>
        /// Indent of the file
        /// </summary>
        public bool Indent { get; set; } = true;

        /// <summary>
        /// Make a backup before replacing the old version
        /// </summary>
        public bool Backup { get; set; } = true;

        public SettingsManager_Instance() { }

        /// <summary>
        /// Get setting
        /// </summary>
        public T GetSetting()
        {
            return setting;
        }

        /// <summary>
        /// Set setting
        /// </summary>
        public void SetSetting(T setting)
        {
            this.setting = setting;
        }

        /// <summary>
        /// Clear setting
        /// </summary>
        public void Clear()
        {
            this.setting = default;
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public void Save(string path)
        {
            if (Backup)
            {
                string bakPath = path + ".bak";
                // Replace old backup
                if (File.Exists(bakPath))
                    File.Delete(bakPath);
                if (File.Exists(path))
                    File.Move(path, bakPath);
            }

            File.WriteAllText(path, GenerateFileData());
        }

        /// <summary>
        /// Save settings to specified file
        /// </summary>
        public void Save(string path, T setting)
        {
            SetSetting(setting);
            Save(path);
        }

        /// <summary>
        /// Load settings from specified path
        /// </summary>
        public bool Load(string path, out T output)
        {
            if (File.Exists(path))
            {
                // Load settings from raw data
                string fileData = File.ReadAllText(path);
                if (string.IsNullOrEmpty(fileData))
                {
                    throw new FileLoadException("Unable to read data from specified file. Aborting");
                }

                setting = Deserialize(fileData);
                output = setting;
                return true;
            }
            else
            {
                output = default;
                return false;
            }
        }

        /// <summary>
        /// Generate file data to be saved in file
        /// </summary>
        public string GenerateFileData()
        {
            return Serialize(setting, Indent);
        }

        /// <summary>
        /// deserialize data
        /// </summary>
        private static T Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// serialize data
        /// </summary>
        private static string Serialize(T data, bool indent)
        {
            return JsonConvert.SerializeObject(data, indent ? Formatting.Indented : Formatting.None);
        }
    }
}
