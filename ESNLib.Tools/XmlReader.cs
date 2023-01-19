using System;
using System.IO;
using System.Net;
using System.Xml;

namespace ESNLib.Tools
{
    /// <summary>
    /// Manage reading a <see cref="XmlDocument"/> from a file or from the web
    /// </summary>
    public abstract class XmlWebReader
    {
        private XmlWebReader() { }

        /// <summary>
        /// Read a <see cref="XmlDocument"/> from a file
        /// </summary>
        /// <param name="path">Path to the xml file</param>
        /// <returns><see cref="XmlDocument"/> read or null if failed</returns>
        public static XmlDocument ReadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Path invalid");
                return null;
            }

            string content = string.Empty;
            try
            {
                content = File.ReadAllText(path);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Unable to read file : " + ex);
                return null;
            }

            // Create xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);

            return doc;
        }
        
        /// <summary>
        /// Read a <see cref="XmlDocument"/> from a web URL
        /// </summary>
        /// <param name="url">URL to read the xml document</param>
        /// <returns><see cref="XmlDocument"/> read or null if failed</returns>
        public static XmlDocument ReadFromWeb(string url)
        {
            try
            {
                // Read file
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(url);
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();

                // Create xml
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                return doc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

    }
}
