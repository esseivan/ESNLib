using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    public abstract class MiscTools
    {

        /// <summary>
        /// Convert decimal format to engineer format
        /// </summary>
        public static string DecimalToEngineer(double Value, bool space = false)
        {
            return DecimalToEngineer(Value, 3, space);
        }

        /// <summary>
        /// Convert decimal format to engineer format
        /// </summary>
        public static string DecimalToEngineer(double Value, int Digits, bool space = false)
        {
            if (double.IsInfinity(Value) || double.IsNaN(Value))
                return null;

            bool isNeg = false;

            if (Value < 0)
            {
                isNeg = true;
                Value = -Value;
            }

            if (Value == 0)
            {
                return $"0{(space ? " " : "")}";
            }

            string Output = "";
            short PowS = 0;

            while (Value < 1)
            {
                Value *= 1000;
                PowS--;
            }

            while (Value >= 1000)
            {
                Value /= 1000;
                PowS++;
            }

            Value = Math.Round(Value, Digits);

            switch (PowS)
            {
                case -8:
                    Output = $"{Value}{(space ? " " : "")}y";
                    break;
                case -7:
                    Output = $"{Value}{(space ? " " : "")}z";
                    break;
                case -6:
                    Output = $"{Value}{(space ? " " : "")}a";
                    break;
                case -5:
                    Output = $"{Value}{(space ? " " : "")}f";
                    break;
                case -4:
                    Output = $"{Value}{(space ? " " : "")}p";
                    break;
                case -3:
                    Output = $"{Value}{(space ? " " : "")}n";
                    break;
                case -2:
                    Output = $"{Value}{(space ? " " : "")}μ";
                    break;
                case -1:
                    Output = $"{Value}{(space ? " " : "")}m";
                    break;
                case 0:
                    Output = $"{Value}{(space ? " " : "")}";
                    break;
                case 1:
                    Output = $"{Value}{(space ? " " : "")}k";
                    break;
                case 2:
                    Output = $"{Value}{(space ? " " : "")}M";
                    break;
                case 3:
                    Output = $"{Value}{(space ? " " : "")}G";
                    break;
                case 4:
                    Output = $"{Value}{(space ? " " : "")}T";
                    break;
                case 5:
                    Output = $"{Value}{(space ? " " : "")}P";
                    break;
                case 6:
                    Output = $"{Value}{(space ? " " : "")}E";
                    break;
                case 7:
                    Output = $"{Value}{(space ? " " : "")}Z";
                    break;
                case 8:
                    Output = $"{Value}{(space ? " " : "")}Y";
                    break;
                default:
                    Output = $"{Value}E{PowS}{(space ? " " : "")}";
                    break;
            }

            if (isNeg)
                Output = "-" + Output;

            return Output;
        }

        /// <summary>
        /// Convert engineer format to decimal
        /// </summary>
        public static double EngineerToDecimal(string Text)
        {
            if (Text == string.Empty || Text == null)
            {
                //MessageBox.Show("Missing value", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return double.NaN;
            }

            short PowS = 0;

            char PowSString = Text.LastOrDefault();
            if (double.TryParse(Text, out double temp))
            {
                return temp;
            }

            if (!double.TryParse(Text.Remove(Text.Length - 1, 1), out double Value))
            {
                //MessageBox.Show("Invalid resistor value format\n" + Text.Remove(Text.Length - 1, 1), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return double.NaN;
            }

            while (Value < 1)
            {
                Value *= 1000;
                PowS--;
            }

            while (Value >= 1000)
            {
                Value /= 1000;
                PowS++;
            }

            switch (PowSString)
            {
                case 'm':
                    PowS -= 1;
                    break;
                case 'k':
                    PowS += 1;
                    break;
                case 'M':
                    PowS += 2;
                    break;
                case 'G':
                    PowS += 3;
                    break;
                default:
                    {
                        //MessageBox.Show("Invalid resistor value format.\nAccepted prefixes are 'm', 'k', 'M', 'G'. Use as following :\n24.56k", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return double.NaN;
                    }
            }

            Value *= Math.Pow(10, 3 * PowS);

            return Value;
        }

        /// <summary>
        /// Get error percent
        /// </summary>
        public static double GetErrorPercent(double Value, double TrueValue)
        {
            if (TrueValue == 0 || double.IsNaN(TrueValue))
                return double.NaN;

            return (100 * (Value - TrueValue) / TrueValue);
        }

        public enum FileSize
        {
            B = 0,
            kB = 1,
            MB = 2,
            GB = 3,
            TB = 4,
        }

        public static string GetFilesSizeString(IEnumerable<string> paths, int decimals = 2)
        {
            double total = 0;
            foreach (string path in paths)
            {
                total += new FileInfo(path).Length;
            }

            FileSize unit = 0;
            while (total >= 1024)
            {
                total = Math.Round(total / 1024, decimals);
                unit++;
            }
            return $"{total}{unit.ToString()}";
        }

        public static string GetFileSizeString(string path, int decimals = 2)
        {
            FileSize unit = 0;
            double fileSize = new FileInfo(path).Length;
            while (fileSize >= 1024)
            {
                fileSize = Math.Round(fileSize / 1024, decimals);
                unit++;
            }
            return $"{fileSize}{unit.ToString()}";
        }

        public static async Task<bool> DownloadFile(string webPath)
        {
            return await DownloadFile(webPath,
                Path.GetTempPath(),
                Path.GetFileName(webPath),
                Path.GetExtension(webPath),
                false);
        }

        public static async Task<bool> DownloadFile(string webPath, bool RunAfterDownload)
        {
            return await DownloadFile(webPath,
                Path.GetTempPath(),
                Path.GetFileName(webPath),
                Path.GetExtension(webPath),
                RunAfterDownload);
        }

        public static async Task<bool> DownloadFile(string webPath, string fileName, bool RunAfterDownload)
        {
            return await DownloadFile(webPath,
                Path.GetTempPath(),
                fileName,
                Path.GetExtension(webPath),
                RunAfterDownload);
        }

        public static async Task<bool> DownloadFile(string webPath, string storePath, string fileName, bool RunAfterDownload)
        {
            return await DownloadFile(webPath,
                storePath,
                fileName,
                Path.GetExtension(webPath),
                RunAfterDownload);
        }

        public static async Task<bool> DownloadFile(string webPath, string storePath, string fileName, string extension, bool RunAfterDownload)
        {
            string filePath = Path.ChangeExtension(Path.Combine(storePath, Path.GetFileNameWithoutExtension(fileName)), extension);
            WebClient webClient = new WebClient();
            await webClient.DownloadFileTaskAsync(new Uri(webPath), filePath);
            FileInfo info = new FileInfo(filePath);
            if (info.Length != 0)
            {
                if (RunAfterDownload)
                {
                    var process = Process.Start(filePath);
                    await Task.Delay(300);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
