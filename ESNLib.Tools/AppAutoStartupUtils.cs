using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace ESNLib.ToolsWinForms
{
    /// <summary>
    /// Contain static functions to manage your application automatic startup on windows boot
    /// </summary>
    public abstract class AppAutoStartupUtils
    {
        /// <summary>
        /// Returns the executable path
        /// </summary>
        private static string GetExecPath()
        {
            Assembly assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            return Path.GetDirectoryName(assembly.Location) ?? string.Empty;
        }

        /// <summary>
        /// Set the startup value
        /// </summary>
        /// <param name="appName">Your unique app name</param>
        /// <param name="runOnStartup">Whether it is enabled on not</param>
        /// <param name="args">Optionnal arguments. Leave empty if none required</param>
        /// <param name="path">Optionnal path to the executable. If left empty (default), the current executable path is used (Application.ExecutablePath)</param>
        public static void SetStartup(string appName, bool runOnStartup, string args = "", string path = "")
        {
            string key = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(key, true);

            if (rk == null)
                throw new Exception("Unable to get key : " + key);

            if (runOnStartup)
            {
                string cmd = $"\"{GetExecPath()}\"";
                if (!string.IsNullOrEmpty(args))
                    cmd += $" {args}";
                rk.SetValue(appName, cmd);
            }
            else
                rk.DeleteValue(appName, false);
        }

        /// <summary>
        /// Returns the startup command
        /// </summary>
        public static string GetStartupCmd(string appName)
        {
            string key = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(key, false);

            if (rk == null)
                throw new Exception("Unable to get key : " + key);

            return (string)rk.GetValue(appName, null);
        }

        /// <summary>
        /// Returns whether the startup is enabled
        /// </summary>
        public static bool GetStartupState(string appName)
        {
            string key = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(key, false);

            if (rk == null)
                throw new Exception("Unable to get key : " + key);

            return rk.GetValue(appName, null) != null;
        }

    }
}
