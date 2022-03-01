using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    /// <summary>
    /// Logging manager. Supports streams
    /// </summary>
    public class LoggerStream<T> : Logger
    {
        /// <summary>
        /// The output stream if WriteMode is WriteMode.Stream
        /// </summary>
        public StreamLogger<T>? OutputStream { get; set; } = null;

        public LoggerStream() : base()
        {

        }

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
