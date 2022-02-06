using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    /// <summary>
    /// Manage logging
    /// </summary>
    public class Logger_Extended<T> : Logger
    {
        /// <summary>
        /// The output stream if WriteMode is WriteMode.Stream
        /// </summary>
        public StreamLogger<T>? OutputStream { get; set; } = null;

        public Logger_Extended()
        {

        }

        protected override string CheckFile(string path)
        {
            if (WriteMode == WriteModes.Stream)
                return "true";

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
                if (WriteMode == WriteModes.Write)
                {
                    File.WriteAllText(path, string.Empty);
                }
            }

            return path;
        }

        /// <summary>
        /// Write log with custom text
        /// </summary>
        public override bool WriteLog(string data, string log_level)
        {
            if (!Enabled)
                return false;

            if (WriteMode == WriteModes.Stream)
            {
                if (OutputStream == null)
                {
                    LastException = new ArgumentException("Output stream is not set !", nameof(OutputStream));
                    return false;
                }
            }

            var lines = data.Replace("\r", "").Split('\n');

            string output = string.Empty;
            string suffix = string.Empty;
            string level = log_level;

            if (level == "None" || level == string.Empty)
            {
                level = string.Empty;
            }
            else
            {
                level = $"[{level}] ".PadRight(8);
            }

            switch (PrefixMode)
            {
                case PrefixModes.None:
                    break;
                case PrefixModes.RunTime:
                    suffix = (DateTime.Now - creationTime).TotalSeconds.ToString("000000.000");
                    break;
                case PrefixModes.CurrentTime:
                    suffix = DateTime.Now.ToString("hh:mm:ss");
                    break;
                case PrefixModes.Custom:
                    suffix = CustomPrefix;
                    break;
                default:
                    break;
            }

            if (suffix != string.Empty)
            {
                suffix = $"[{suffix}] ";
            }

            foreach (var line in lines)
            {
                output += $"{suffix}{level}{line}";
            }

            if (WriteMode == WriteModes.Stream)
            {
                OutputStream.WriteData(output);
            }
            if (FilenameMode != FilenamesModes.None)
            {
                File.AppendAllText(outputPath, output + Environment.NewLine);
            }
            return true;
        }
    }

}
