using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    /// <summary>
    /// Manage logging for your application
    /// </summary>
    public class Logger : IDisposable
    {
        /// <summary>
        /// File path (.log is default extension)
        /// </summary>
        public string FilePath { get; set; } = string.Empty;
        /// <summary>
        /// How is the filaneme determined
        /// </summary>
        public FilenamesModes FilenameMode { get; set; } = FilenamesModes.FileName_DateSuffix;
        /// <summary>
        /// Prefix of the lines in the log
        /// </summary>
        public PrefixModes PrefixMode { get; set; } = PrefixModes.RunTime;
        /// <summary>
        /// Custom prefix when the mode is set to Custom
        /// </summary>
        public string CustomPrefix { get; set; } = "[Custom Prefix]";
        /// <summary>
        /// Write mode. Default is Append
        /// </summary>
        public WriteModes WriteMode { get; set; } = WriteModes.Append;

        /// <summary>
        /// Log level filter. Only higher level logs are processed. Default is disabled (All)
        /// </summary>
        public LogLevels LogLeveFilter { get; set; } = LogLevels.All;

        /// <summary>
        /// Define the DateTime format for the file name with DateSuffix set. Default value is "yyyy_MM_dd__HH_mm_ss"
        /// </summary>
        public string DateSuffixFormat { get; set; } = "yyyy_MM_dd__HH_mm_ss";
        /// <summary>
        /// Define the right padding for the log level prefixes (Global prefixes not included). Default is 8
        /// </summary>
        public int PrefixPadding { get; set; } = 8;

        /// <summary>
        /// Define the runtime prefix format. Default is "000000.000"
        /// </summary>
        public string RuntimePrefixFormat { get; set; } = "000000.000";
        /// <summary>
        /// Define the current time prefix format. Default is "HH:mm:ss.f"
        /// </summary>
        public string CurrentTimePrefixFormat { get; set; } = "HH:mm:ss.f";

        /// <summary>
        /// Error occurred
        /// </summary>
        public bool HasError { get; set; }

        protected bool enabled = false;

        /// <summary>
        /// Is the logger enabled
        /// </summary>
        public bool Enabled { get => enabled; }

        /// <summary>
        /// Log pending to be written. Happens when WriteLog is called but logger is disabled
        /// </summary>
        protected StringBuilder pendingLog = null;

        public delegate void LoggerEventHandler(object sender, LoggerEventArgs e);
        /// <summary>
        /// Event called when a text is written to the log
        /// </summary>
        public event LoggerEventHandler OnLogWrite;
        /// <summary>
        /// Invoke the OnLogWrite event
        /// </summary>
        protected void OnLogWriteInvoke(object sender, string data) => OnLogWrite?.Invoke(sender, new LoggerEventArgs(data));


        /// <summary>
        /// Last exception occurred. Resets HasError flags when read
        /// </summary>
        public Exception LastException
        {
            get
            {
                HasError = false;
                return LastException;
            }
            protected set
            {
                if (LastException != null)
                    HasError = true;
                LastException = value;
            }
        }

        /// <summary>
        /// Output path generated by the Enable method and used by WriteLog
        /// </summary>
        protected string outputPath = string.Empty;

        /// <summary>
        /// Returns the path to the output file, once verified with Enable function
        /// </summary>
        public string FileOutputPath => outputPath;

        /// <summary>
        /// Define the creation time of the logger. Can be reset
        /// </summary>
        public DateTime CreationTime { get; protected set; }

        /// <summary>
        /// Define how the log filename is generated
        /// </summary>
        public enum FilenamesModes
        {
            /// <summary>
            /// Don't log to file
            /// </summary>
            None = 0,
            /// <summary>
            /// Log to specified FileName
            /// </summary>
            FileName = 1,
            /// <summary>
            /// Log to specified fileName with dateTime suffix
            /// </summary>
            FileName_DateSuffix = 2,
            /// <summary>
            /// Keep the 2 last logs name last and previous
            /// </summary>
            FileName_LastPrevious = 3
        }

        /// <summary>
        /// Define the suffix of each log
        /// </summary>
        public enum PrefixModes
        {
            /// <summary>
            /// No suffix
            /// </summary>
            None = 0,
            /// <summary>
            /// Time elapsed from the start
            /// </summary>
            RunTime = 1,
            /// <summary>
            /// Current time
            /// </summary>
            CurrentTime = 2,
            /// <summary>
            /// Custom text
            /// </summary>
            Custom = 3,
        }

        /// <summary>
        /// Define the log levels and their priority
        /// </summary>
        public enum LogLevels
        {
            None = 0,

            Fatal = 10,
            Error = 20,
            Warn = 30,

            User = 35,

            Debug = 40,
            Port = 42,
            Trace = 50,

            All = 255,
        }

        /// <summary>
        /// Define how to write the log
        /// </summary>
        [Flags]
        public enum WriteModes
        {
            /// <summary>
            /// Write over existing file
            /// </summary>
            Write = 1,
            /// <summary>
            /// Append to exitsing file
            /// </summary>
            Append = 2,
            /// <summary>
            /// Output to a stream
            /// </summary>
            Stream = 4,
        }

        /// <summary>
        /// Call the Enable function before to write to log
        /// </summary>
        public Logger()
        {
            ResetCreationTime();
        }

        /// <summary>
        /// Call the Enable function before to write to log
        /// </summary>
        public Logger(string path) : this()
        {
            FilePath = path;
        }

        /// <summary>
        /// Set the creation time to DateTime.Now
        /// </summary>
        public void ResetCreationTime()
        {
            CreationTime = DateTime.Now;
        }

        /// <summary>
        /// Returns %appdata%\'Manufacturer'\'ProductName'\logs\
        /// </summary>
        public static string GetDefaultLogPath(string Manufacturer, string ProductName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"{Manufacturer}\{ProductName}\logs\");
        }

        /// <summary>
        /// MUST be called BEFORE WriteLog. Check if writelog can be called safely AND generate final path. Reset the creationTime when enabling
        /// </summary>
        /// <returns>Returns false if the logger couldn't be enabled. Check that the 'FilePath' is set and the modes are corrects</returns>
        public bool Enable()
        {
            switch (FilenameMode)
            {
                case FilenamesModes.FileName:
                    // If string empty, invalid FilePath
                    if (string.IsNullOrEmpty(FilePath))
                        return false;

                    // Set final output path
                    outputPath = FilePath;
                    break;

                case FilenamesModes.FileName_DateSuffix:
                    // If string empty, invalid FilePath
                    if (string.IsNullOrEmpty(FilePath))
                        return false;

                    // Save extension to set it later
                    string extension = Path.GetExtension(FilePath);
                    // Remove extension
                    outputPath = Path.ChangeExtension(FilePath, null);
                    // Add dateTime
                    outputPath += "_" + DateTime.Now.ToString(DateSuffixFormat);
                    // Add extension
                    outputPath = Path.ChangeExtension(outputPath, extension);

                    break;
                case FilenamesModes.FileName_LastPrevious:
                    // If string empty, invalid FilePath
                    if (string.IsNullOrEmpty(FilePath))
                        return false;

                    // Save extension to set it later
                    extension = Path.GetExtension(FilePath);
                    // Remove extension
                    outputPath = Path.ChangeExtension(FilePath, null);
                    // Add suffix
                    string previousPath = outputPath + "_previous";
                    outputPath += "_current";
                    // Add extension
                    outputPath = Path.ChangeExtension(outputPath, extension);
                    previousPath = Path.ChangeExtension(previousPath, extension);

                    // Rename last file
                    try
                    {
                        if (File.Exists(outputPath))
                        {
                            if (File.Exists(previousPath))
                            {
                                File.Delete(previousPath);
                            }
                            File.Move(outputPath, previousPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        LastException = ex;
                        return false;
                    }

                    break;

                case FilenamesModes.None:
                // No filepath to generate
                default:
                    break;
            }

            outputPath = CheckFile(outputPath);
            ResetCreationTime();

            enabled = true;
            return true;
        }

        /// <summary>
        /// Disable writelog function. Note that when re-enabling, a new log file may be created
        /// </summary>
        public void Disable()
        {
            enabled = false;
        }

        /// <summary>
        /// Check and validate the path to the file
        /// </summary>
        public virtual string CheckFile(string path)
        {
            if (WriteMode == WriteModes.Stream)
                throw new InvalidOperationException("Invalid write mode. Use Logger<T> for WriteMode.Stream");

            if (string.IsNullOrEmpty(path))
                return path;

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            // If no extension, set it to .log
            if (!Path.HasExtension(path))
            {
                path = Path.ChangeExtension(path, "log");
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            else
            {
                // if writemode set to write, clear file
                if (WriteMode == WriteModes.Write)
                {
                    File.WriteAllText(path, string.Empty);
                }
            }

            return path;
        }

        /// <summary>
        /// Write log with default level (debug). Level filter may skip this
        /// </summary>
        public bool Write(string data)
        {
            return Write(data, LogLevels.Debug);
        }

        /// <summary>
        /// Write log with specified level. Level filter may skip this
        /// </summary>
        /// <returns>Returns false if logger is not enabled or log is filtered. Call Enable() if not done</returns>
        public bool Write(string data, LogLevels logLevel)
        {
            // If filtered
            if ((int)logLevel > (int)LogLeveFilter)
                return false;

            return Write(data, logLevel.ToString());
        }

        /// <summary>
        /// Write log with custom text. No filter is applied here
        /// </summary>
        /// <returns>Returns false if logger is not enabled. Call Enable()</returns>
        public virtual bool Write(string data, string logLevelName)
        {
            if (WriteMode == WriteModes.Stream)
                throw new InvalidOperationException("Invalid write mode. Use Logger<T> for WriteMode.Stream");

            string output = GenerateLogLines(data, logLevelName);

            // Logging disabled
            if (!enabled)
            {
                // Add to pending log
                if (pendingLog == null)
                    pendingLog = new StringBuilder();
                pendingLog = pendingLog.Append(output);
                return false;
            }

            // Write pending log before, if any
            if (pendingLog != null)
            {
                _write(pendingLog.ToString());
                pendingLog = null;
            }

            _write(output);

            return true;
        }

        /// <summary>
        /// Actually write log. Private function
        /// </summary>
        protected void _write(string data, bool invoke = true)
        {
            if (invoke)
                OnLogWriteInvoke(this, data);

            if ((WriteMode.HasFlag(WriteModes.Append) ||
                WriteMode.HasFlag(WriteModes.Write)) &&
                FilenameMode != FilenamesModes.None)
            {
                File.AppendAllText(outputPath, data);
            }
        }

        /// <summary>
        /// Generate the lines for the log
        /// </summary>
        public string GenerateLogLines(string log, string logLevelName)
        {
            // Split by lines
            var lines = log.Replace("\r", "").Split('\n');

            string prefix = string.Empty;
            string loglevelPrefix = logLevelName;

            // Set the log level prefix string
            if (loglevelPrefix == "None" || loglevelPrefix == string.Empty)
                loglevelPrefix = string.Empty;
            else
                loglevelPrefix = $"[{loglevelPrefix}] ";
            // Set to be all the same length to have a better readability
            loglevelPrefix.PadRight(PrefixPadding);

            switch (PrefixMode)
            {
                case PrefixModes.RunTime:
                    prefix = (DateTime.Now - CreationTime).TotalSeconds.ToString(RuntimePrefixFormat);
                    break;
                case PrefixModes.CurrentTime:
                    prefix = DateTime.Now.ToString(CurrentTimePrefixFormat);
                    break;
                case PrefixModes.Custom:
                    prefix = CustomPrefix;
                    break;
                case PrefixModes.None:
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(prefix))
            {
                prefix = $"[{prefix}] ";
            }

            StringBuilder outputLog = new StringBuilder();
            foreach (var line in lines)
            {
                outputLog.Append($"{prefix}{loglevelPrefix}{line}{Environment.NewLine}");
            }

            return outputLog.ToString();
        }

        public virtual void Dispose()
        {
            Disable();
        }

        public class LoggerEventArgs : EventArgs
        {
            public string Log { get; private set; } = string.Empty;

            public LoggerEventArgs(string Text)
            {
                this.Log = Text;
            }
        }
    }

}
