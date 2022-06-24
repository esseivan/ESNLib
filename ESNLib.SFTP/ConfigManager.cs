using ESNLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.SFTP
{
    public partial class ConfigManager
    {
        /// <summary>
        /// Import config from specified file
        /// </summary>
        /// <param name="Path"></param>
        public static Dictionary<string, BaseConfig> ImportConfig(string Path)
        {
            try
            {
                SettingsManager.LoadFrom(Path, out Dictionary<string, BaseConfig> output);
                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load config", ex);
            }
        }

        /// <summary>
        /// Export config to specified file
        /// </summary>
        public static void ExportConfig(string Path, BaseConfig[] Configs)
        {
            try
            {
                SettingsManager.SaveTo(Path, Configs, false, false);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save config", ex);
            }
        }
    }
}
