using ESNLib.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

/* Example :
 *
 * private void CheckUpdates()
 *      {
 *          ESNLib.Controls.UpdateChecker update = new ESNLib.Controls.UpdateChecker("http://.../.../.../version.xml", this.ProductVersion);
 *
 *          if (update.NeedUpdate())
 *          {   // Update available
 *              var result = update.Result;
 *              DialogResult dr = showMessage_func($"Update is available, do you want to visit the website ?\nCurrent : {result.CurrentVersion}\nLast : {result.LastVersion}", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
 *              if (dr == DialogResult.Yes)
 *                  result.OpenUpdateWebsite();
 *          }
 *          if (update.Result.ErrorOccured)
 *              throw update.Result.Error;
 *      }
 */

/* Version.xml file example
 * <?xml version="1.0" encoding="utf-8" ?>
 *	<Feed>
 *	<version>1.1.0</version>
 *	<url>http://www.website.com/softwares/publish.html</url>
 *	<silent>http://www.website.com/softwares/silentInstaller.msi</silent>
 *  <name>productName</name>
 *	</Feed>
 *
 *	Silent installer is used to run a quick update
 */

namespace ESNLib.Tools.WinForms
{
    public class UpdateChecker
    {
        /// <summary>
        /// Website where is located the file
        /// </summary>
        public string FileUrl { get; set; } = "";

        /// <summary>
        /// Result of the check
        /// </summary>
        public CheckUpdateResult Result { get; private set; } = new CheckUpdateResult();

        /// <summary>
        /// Actual version
        /// </summary>
        public string ProductVersion { get; set; }

        public UpdateChecker() { }

        public UpdateChecker(string FileUrl, string ProductVersion)
        {
            this.FileUrl = FileUrl;
            this.ProductVersion = ProductVersion;
        }

        /// <summary>
        /// Check for updates
        /// </summary>
        public bool CheckUpdates()
        {
            return UpdateCheck();
        }

        /// <summary>
        /// Check for updates
        /// </summary>
        public bool CheckUpdates(string FileUrl, string ProductVersion)
        {
            this.FileUrl = FileUrl;
            this.ProductVersion = ProductVersion;
            return UpdateCheck();
        }

        /// <summary>
        /// Check if an update is available
        /// </summary>
        private bool UpdateCheck()
        {
            Result = new CheckUpdateResult();

            try
            {
                // Read file
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(FileUrl);
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();

                // Create xml
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                // Get tag version
                XmlNodeList versionNode = doc.GetElementsByTagName("version");
                if (versionNode.Count != 0)
                {
                    Result.lastVersion = new Version(versionNode[0].InnerText);
                    Result.currentVersion = new Version(ProductVersion);

                    // Check versions
                    if (Result.lastVersion > Result.currentVersion)
                    { // Update available
                        Result.needUpdate = true;

                        // Get the download path
                        XmlNodeList urlNode = doc.GetElementsByTagName("url");
                        if (urlNode.Count != 0)
                        {
                            Result.updateURL = urlNode[0].InnerText;
                        }

                        // Get the silent update path
                        urlNode = doc.GetElementsByTagName("silent");
                        if (urlNode.Count != 0)
                        {
                            Result.silentUpdateURL = urlNode[0].InnerText;
                        }

                        // Get the filename
                        urlNode = doc.GetElementsByTagName("name");
                        if (urlNode.Count != 0)
                        {
                            Result.filename = urlNode[0].InnerText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result.errorOccured = true;
                Result.error = ex;
                return false;
            }
            return Result.NeedUpdate;
        }

        /// <summary>
        /// Result of an update check
        /// </summary>
        public class CheckUpdateResult
        {
            internal string silentUpdateURL;
            internal string updateURL;
            internal Version currentVersion;
            internal Version lastVersion;
            internal bool needUpdate = false;
            internal Exception error = null;
            internal bool errorOccured = false;
            internal string filename;

            /// <summary>
            /// The current version
            /// </summary>
            public Version CurrentVersion
            {
                get => currentVersion;
            }

            /// <summary>
            /// The last release version
            /// </summary>
            public Version LastVersion
            {
                get => lastVersion;
            }

            /// <summary>
            /// Wheter the current version is lower to the release version
            /// </summary>
            public bool NeedUpdate
            {
                get => needUpdate;
            }

            /// <summary>
            /// The URL of the publish page
            /// </summary>
            public string UpdateURL
            {
                get => updateURL;
            }

            /// <summary>
            /// Indicate if an error occurred
            /// </summary>
            public bool ErrorOccurred
            {
                get => errorOccured;
            }

            /// <summary>
            /// The error that occurred
            /// </summary>
            public Exception Error
            {
                get => error;
            }

            /// <summary>
            /// The URL of the silent installer
            /// </summary>
            public string SilentUpdateURL
            {
                get => silentUpdateURL;
            }

            /// <summary>
            /// Open the website
            /// </summary>
            public void OpenUpdateWebsite()
            {
                Process.Start(updateURL);
            }

            /// <summary>
            /// Download and run the update
            /// </summary>
            public async Task<bool> DownloadUpdate()
            {
                return await DownloadUpdate(Path.GetTempPath());
            }

            /// <summary>
            /// Download and run the update
            /// </summary>
            public async Task<bool> DownloadUpdate(string downloadPath)
            {
                WebClient webClient = new WebClient();
                await webClient.DownloadFileTaskAsync(
                    new Uri(SilentUpdateURL),
                    downloadPath + filename + ".msi"
                );
                FileInfo info = new FileInfo(downloadPath + filename + ".msi");
                if (info.Length != 0)
                {
                    var process = Process.Start(downloadPath + filename + ".msi");
                    await Task.Delay(300);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    /// <summary>
    /// Contains predefined methods to ask user what to do. Optionnal use
    /// </summary>
    public class UpdateChecker_Runner
    {
        private UpdateChecker_Runner() { }

        public enum CheckUpdateAndAsk_Result
        {
            None = 0,
            NoUpdate = 1,
            Error = 2,
            UnknownError = 3,
            User_DoNothing = 10,
            User_OpenWebsite = 11,
            User_Install = 12,
        }

        /// <summary>
        /// Return App version from System.Reflection.Assembly.GetEntryAssembly()
        /// </summary>
        public static string GetAppVersion(System.Reflection.Assembly assembly)
        {
            return assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// Execute both CheckUpdateAndAsk and ProcessResult
        /// </summary>
        /// <param name="url">URL of the version.xml file</param>
        /// <param name="assembly">Assembly to retrieve version from</param>
        /// <param name="applicationExit">Function to exit application</param>
        /// <param name="showMsg">Function to show message</param>
        public static void CheckUpdateAndProcess(
            string url,
            System.Reflection.Assembly assembly,
            Action applicationExit,
            Action<string, string, int> showMsg
        )
        {
            var r = CheckUpdateAndAsk(url, GetAppVersion(assembly));
            ProcessResult(r, applicationExit, showMsg);
        }

        /// <summary>
        /// Execute both CheckUpdateAndAsk and ProcessResult
        /// </summary>
        /// <param name="url">URL of the version.xml file</param>
        /// <param name="currentVersion">Actual version of the app</param>
        /// <param name="applicationExit">Function to exit application</param>
        /// <param name="showMsg">Function to show message</param>
        public static void CheckUpdateAndProcess(
            string url,
            string currentVersion,
            Action applicationExit,
            Action<string, string, int> showMsg
        )
        {
            var r = CheckUpdateAndAsk(url, currentVersion);
            ProcessResult(r, applicationExit, showMsg);
        }

        /// <summary>
        /// Check for update and ask user what action to do, the result is then returned
        /// <para>Return result is object array, size of 3 :</para>
        /// <para>0 : type is CheckUpdateResult (or null if error ocurred at bad place)</para>
        /// <para>1 : type is CheckUpdateAndAsk_Result</para>
        /// <para>2 : type is Exception (if ocurred) or null</para>
        /// </summary>
        public static object[] CheckUpdateAndAsk(string url, string currentVersion)
        {
            object[] output = new object[3];
            output[0] = null;
            output[1] = CheckUpdateAndAsk_Result.None;
            output[2] = null;

            try
            {
                //showMessage_func(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
                UpdateChecker update = new UpdateChecker(url, currentVersion);
                update.CheckUpdates();
                if (update.Result.ErrorOccurred)
                {
                    output[0] = update.Result;
                    output[1] = CheckUpdateAndAsk_Result.Error;
                    output[2] = update.Result.Error;
                    return output;
                }

                if (update.CheckUpdates())
                { // Update available
                    var result = update.Result;
                    output[0] = result;

                    Dialog.DialogConfig dialogConfig = new Dialog.DialogConfig()
                    {
                        Message =
                            $"Update is available, do you want to download ?\nCurrent: {result.CurrentVersion}\nLast: {result.LastVersion}",
                        Title = "Update available",
                        Button1 = Dialog.ButtonType.Custom1,
                        CustomButton1Text = "Visit website",
                        Button2 = Dialog.ButtonType.Custom2,
                        CustomButton2Text = "Download and install",
                        Button3 = Dialog.ButtonType.Cancel,
                    };

                    var dr = Dialog.ShowDialog(dialogConfig);

                    if (dr.DialogResult == Dialog.DialogResult.Custom1)
                    {
                        // Visit website
                        output[1] = CheckUpdateAndAsk_Result.User_OpenWebsite;
                    }
                    else if (dr.DialogResult == Dialog.DialogResult.Custom2)
                    {
                        // Download and install
                        output[1] = CheckUpdateAndAsk_Result.User_Install;
                    }
                    else
                    {
                        output[1] = CheckUpdateAndAsk_Result.User_DoNothing;
                    }
                }
                else
                {
                    output[1] = CheckUpdateAndAsk_Result.NoUpdate;
                }
            }
            catch (Exception ex)
            {
                output[1] = CheckUpdateAndAsk_Result.UnknownError;
                output[2] = ex;
            }

            return output;
        }

        /// <summary>
        /// Process the result of CheckUpdateAndAsk
        /// </summary>
        /// <param name="result"></param>
        /// <param name="showMessage_func"></param>
        /// <param name="applicationExit"></param>
        public static async void ProcessResult(
            object[] result,
            Action applicationExit,
            Action<string, string, int> showMsg
        )
        {
            UpdateChecker.CheckUpdateResult checkResult = (UpdateChecker.CheckUpdateResult)
                result[0];
            CheckUpdateAndAsk_Result askResult = (CheckUpdateAndAsk_Result)result[1];
            Exception ex = (Exception)result[2];

            switch (askResult)
            {
                case CheckUpdateAndAsk_Result.NoUpdate:
                    showMsg?.Invoke("No new release found", "Information", 64);
                    break;
                case CheckUpdateAndAsk_Result.Error:
                    showMsg?.Invoke(checkResult.Error.ToString(), "Error", 16);
                    break;
                case CheckUpdateAndAsk_Result.UnknownError:
                    showMsg?.Invoke(ex.ToString(), "Error", 16);
                    break;
                case CheckUpdateAndAsk_Result.User_OpenWebsite:
                    checkResult.OpenUpdateWebsite();
                    break;
                case CheckUpdateAndAsk_Result.User_Install:
                    // Download and install
                    if (await checkResult.DownloadUpdate())
                    {
                        applicationExit?.Invoke();
                    }
                    else
                    {
                        showMsg?.Invoke("Unable to download update", "Error", 16);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
