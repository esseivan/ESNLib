using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WinSCP;

namespace ESNLib.SFTP
{
    public class Uploader
    {
        private static string helpText =
            @"upload folders's content using SFTP protocol
usage: <app_name> [BaseConfig path]";

        public static void Main(string[] args)
        {
            // Check in WinScp available
            try
            {
                Session temp = new Session();
                temp.Dispose();
            }
            catch (Exception)
            {
                Console.Error.WriteLine(
                    "WinSCP not available ! Check if it is installer or install from the official website :\nwinscp.net"
                );
                Console.ReadLine();
                Environment.Exit(-1);
                return;
            }

            // If no arguments, show help
            if (args.Length != 1)
            {
                Console.WriteLine(helpText);
            }
            else
            {
                // If generate template argument
                if (args[0] == "--GT")
                {
                    Console.WriteLine("Generating templates then exit");
                    GenerateTemplates();
                    return;
                }

                Dictionary<string, BaseConfig> configs = ConfigManager.ImportConfig(args[0]);
                if (configs == null || configs.Count == 0)
                {
                    Console.WriteLine("Invalid config file");
                    Environment.Exit(2);
                }
                else
                {
                    foreach (BaseConfig baseConfig in configs.Values)
                    {
                        RunUpload(baseConfig);
                    }
                }
            }
        }

        /// <summary>
        /// Run the upload
        /// </summary>
        /// <returns>Wheter or not the upload is successfull</returns>
        public static bool RunUpload(BaseConfig baseConfig)
        {
            // Check file
            if (!baseConfig.IsValid())
            {
                Console.WriteLine("Invalid config : " + baseConfig);
                return false;
            }

            // Run each config
            try
            {
                SessionOptions sessionOptions = null;
                // Used to declare local variables that will die once if exitted
                if (true)
                {
                    string hostKey = baseConfig.HostKeyPath;

                    if (File.Exists(hostKey))
                        hostKey = File.ReadAllText(hostKey);

                    sessionOptions = new SessionOptions()
                    {
                        Protocol = Protocol.Sftp,
                        HostName = baseConfig.Hostname,
                        UserName = baseConfig.Username,
                        SshHostKeyFingerprint = hostKey,
                        SshPrivateKeyPath = baseConfig.PrivateKeyPath,
                        PrivateKeyPassphrase = baseConfig.Passphrase,
                        PortNumber = baseConfig.Port,
                        FtpMode = baseConfig.FtpMode,
                    };
                }

                // Open new session
                using (Session session = new Session())
                {
                    // Enable log
                    string logPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "sftp_upload_log"
                    );
                    if (!Directory.Exists(logPath))
                        Directory.CreateDirectory(logPath);

                    //session.SessionLogPath = (Path.Combine(logPath, "sessionLog.txt"));
                    session.DebugLogPath = (Path.Combine(logPath, "debugLog.txt"));
                    session.DebugLogLevel = -1;

                    // Connect
                    session.Open(sessionOptions);

                    // Sort configs
                    Array.Sort(baseConfig.Configs);
                    foreach (BaseConfig.Config config in baseConfig.Configs)
                    {
                        Console.WriteLine(
                            $"[{DateTime.Now.ToString("yyyy.MM.dd hh.mm.ss")}] Starting new upload"
                        );

                        if (config.SyncModeEnabled)
                            Console.WriteLine(
                                $"Synchronize local {config.LocalPath} to remote {config.RemotePath}"
                            );
                        else
                            Console.WriteLine(
                                $"Upload local {config.LocalPath} to remote {config.RemotePath}"
                            );

                        if (config.FileMask != string.Empty)
                            Console.WriteLine("With file mask : \"" + config.FileMask + "\"");
                        else
                            Console.WriteLine("Without file mask");

                        if (!config.Enabled)
                        {
                            Console.WriteLine("Config disabled");
                            continue;
                        }

                        if (!config.IsValid())
                        {
                            Console.WriteLine("Invalid config");
                            continue;
                        }

                        // Upload files
                        TransferOptions transferOptions = new TransferOptions
                        {
                            TransferMode = config.TransferMode,
                            FileMask = config.FileMask,
                        };

                        if (config.SyncModeEnabled)
                        {
                            // Synchronise
                            SynchronizationResult syncResult = session.SynchronizeDirectories(
                                mode: config.SyncMode,
                                localPath: config.LocalPath,
                                remotePath: config.RemotePath,
                                removeFiles: config.RemoveFiles,
                                mirror: config.Mirror,
                                criteria: config.SyncCriteria,
                                options: transferOptions
                            );

                            WriteResult(syncResult);
                        }
                        else
                        {
                            // Upload
                            TransferOperationResult transfResult = session.PutFiles(
                                localPath: config.LocalPath,
                                remotePath: config.RemotePath,
                                remove: config.RemoveFiles,
                                options: transferOptions
                            );

                            WriteResult(transfResult);
                        }
                    }
                    // Session is automatically closed with the using keyword
                }
                Console.WriteLine(
                    $"[{DateTime.Now.ToString("yyyy.MM.dd hh.mm.ss")}] Upload success"
                );
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine(ex);
                return false;
            }
            return true;
        }

        private static void WriteResult(TransferOperationResult result)
        {
            result.Check();
            Console.WriteLine("Failures: " + result.Failures.Count);
            if (result.Failures.Count != 0)
            {
                foreach (SessionRemoteException item in result.Failures)
                {
                    Console.WriteLine("\t" + item.Message);
                }
            }

            Console.WriteLine("Transfers: " + result.Transfers.Count);
            if (result.Transfers.Count != 0)
            {
                foreach (TransferEventArgs item in result.Transfers)
                {
                    Console.WriteLine("\t" + item.FileName);
                }
            }
            Console.WriteLine("IsSuccess: " + result.IsSuccess);
        }

        private static void WriteResult(SynchronizationResult result)
        {
            result.Check();
            Console.WriteLine("Failures: " + result.Failures.Count);
            if (result.Failures.Count != 0)
            {
                foreach (SessionRemoteException item in result.Failures)
                {
                    Console.WriteLine("\t" + item.Message);
                }
            }

            Console.WriteLine("Downloads: " + result.Downloads.Count);
            if (result.Downloads.Count != 0)
            {
                foreach (TransferEventArgs item in result.Downloads)
                {
                    Console.WriteLine("\t" + item.FileName);
                }
            }
            Console.WriteLine("Uploads: " + result.Uploads.Count);
            if (result.Uploads.Count != 0)
            {
                foreach (TransferEventArgs item in result.Uploads)
                {
                    Console.WriteLine("\t" + item.FileName);
                }
            }
            Console.WriteLine("Removals: " + result.Removals.Count);
            if (result.Removals.Count != 0)
            {
                foreach (RemovalEventArgs item in result.Removals)
                {
                    Console.WriteLine("\t" + item.FileName);
                }
            }
            Console.WriteLine("IsSuccess: " + result.IsSuccess);
        }

        /// <summary>
        /// Generate template files
        /// </summary>
        public static void GenerateTemplates()
        {
            string cfgpath = Path.Combine(Environment.CurrentDirectory, "config.cfg");
            string varpath = Path.Combine(Environment.CurrentDirectory, "var.cfg");
            ConfigManager.ExportConfig(
                cfgpath,
                new BaseConfig[]
                {
                    new BaseConfig()
                    {
                        FtpMode = FtpMode.Passive,
                        HostKeyPath = @"C:\TBD",
                        PrivateKeyPath = @"C:\TBD",
                        Hostname = @"www.hostname.com",
                        Username = "pi",
                        Passphrase = "passphrase",
                        Port = 22,
                        Configs = new BaseConfig.Config[]
                        {
                            new BaseConfig.Config()
                            {
                                LocalPath = @"C:\TBD",
                                RemotePath = @"\var\www\tbd",
                                Mirror = false,
                                RemoveFiles = false,
                                RunPriority = 0,
                                SyncCriteria = SynchronizationCriteria.Either,
                                SyncMode = SynchronizationMode.Remote,
                                TransferMode = TransferMode.Binary
                            }
                        }
                    }
                }
            );
        }
    }
}
