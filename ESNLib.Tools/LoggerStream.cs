using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    /// <summary>
    /// Logging manager that supports <see cref="Stream"/> as output
    /// </summary>
    public class LoggerStream<T> : Logger
    {
        /// <summary>
        /// The output stream when WriteMode is set to WriteMode.Stream
        /// </summary>
        public StreamLogger<T> OutputStream { get; set; } = null;

        /// <summary>
        /// Create an instance of the <see cref="LoggerStream{T}"/>
        /// </summary>
        public LoggerStream() : base()
        {

        }

        /// <summary>
        /// Check and validate the path given. Prepare the file.
        /// </summary>
        /// <param name="path">Path to the log file</param>
        /// <returns>Actual path to the log file, if modified (e.g. added '.log' extension if none specified)</returns>
        public override string CheckFile(string path)
        {
            if (!(WriteMode.HasFlag(WriteModes.Write) ||
                WriteMode.HasFlag(WriteModes.Append)))
                return string.Empty;

            // Create directory if not existing
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            // If no extension, set .log
            if (!Path.HasExtension(path))
            {
                path = Path.ChangeExtension(path, "log");
            }

            // If file not existing, create
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            // Else if file existing and writemode set to write, clear file
            else
            {
                if (WriteMode.HasFlag(WriteModes.Write))
                {
                    File.WriteAllText(path, string.Empty);
                }
            }

            return path;
        }

        /// <summary>
        /// Dispose of the logger. The <see cref="Stream"/> will be disposed as well !
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            if (OutputStream != null)
                (OutputStream.StreamOutput as IDisposable)?.Dispose();
        }

        /// <summary>
        /// Write log with custom text
        /// </summary>
        public override bool Write(string data, string logLevelName)
        {
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

            if (WriteMode.HasFlag(WriteModes.Stream) && OutputStream == null)
            {
                LastException = new ArgumentException("Output stream is not set !", nameof(OutputStream));
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
        /// Write without checking if allowed
        /// </summary>
        /// <param name="data">Text to write</param>
        protected void _write(string data)
        {
            base._write(data);

            if (WriteMode.HasFlag(WriteModes.Stream) && OutputStream != null)
            {
                OutputStream.WriteData(data);
            }
        }
    }

}
