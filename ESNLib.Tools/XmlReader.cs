using System;
using System.IO;
using System.Net;
using System.Xml;

namespace ESNLib.Tools
{
    public class XmlWebReader
    {
        private XmlWebReader() { }

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
