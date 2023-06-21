using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools.WinForms
{
    /// <summary>
    /// Import a csv file into a list of objects. Let the user decide the columns
    /// </summary>
    public class CsvImportAs<T> where T : new()
    {
        /// <summary>
        /// Links between header names and properties of the class
        /// </summary>
        private Dictionary<string, PropertyInfo> propertiesLink = null;
        /// <summary>
        /// Links between header names and properties' name of the class
        /// </summary>
        private Dictionary<string, string> propertiesLinkNames = null;
        /// <summary>
        /// Get the list of properties for this class <see cref="T"/>
        /// </summary>
        public PropertyInfo[] GetProperties()
        {
            return typeof(T).GetProperties();
        }

        /// <summary>
        /// Set linking between the header name and the property
        /// </summary>
        public void SetPropertiesLink(Dictionary<string, PropertyInfo> links)
        {
            propertiesLinkNames = null;
            propertiesLink = links;
        }

        /// <summary>
        /// Set linking between the header name and the property name
        /// </summary>
        public void SetPropertiesLink(Dictionary<string, string> links)
        {
            propertiesLink = null;
            propertiesLinkNames = links;
        }

        /// <summary>
        /// Import the data as the content of a csv file
        /// </summary>
        /// <param name="data">content of the csv, ',' separated</param>
        /// <returns>List of converted items</returns>
        public List<T> ImportData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            // Split by lines
            string[] lines = data.Split('\n');

            // Headers on first line
            string[] headers = lines[0].Split(',');
            lines = lines.Skip(1).ToArray();

            // Generate Properties Info
            List<PropertyInfo> properties = new List<PropertyInfo>();

            foreach (string header in headers)
            {
                var propInfo = typeof(T).GetProperty(header);
                properties.Add(propInfo);
            }

            // Generate T list
            List<T> list = new List<T>();

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                { continue; }

                T item = new T();
                string[] lineItems = line.Split(',');

                for (int i = 0; i < lineItems.Length; i++)
                {
                    if (properties[i] == null) { continue; }
                    PropertyInfo pinfo = properties[i];

                    string lineItem = lineItems[i];
                    pinfo.SetValue(item, Convert.ChangeType(lineItem, pinfo.PropertyType));
                }
                list.Add(item);
            }

            return list;
        }



    }


    ///// <summary>
    ///// Attribute to set a special header name for the property
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    //public class CsvImportAttribute : Attribute
    //{
    //    public string CustomName { get; }
    //    public CsvImportAttribute(string name)
    //    {
    //        this.CustomName = name;
    //    }
    //}
}
