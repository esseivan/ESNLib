using Microsoft.VisualBasic.FileIO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ESNLib.Tools.WinForms
{
    /// <summary>
    /// Import a csv file into a list of objects. Let the user decide the columns
    /// </summary>
    public class CsvImportAs<T>
        where T : new()
    {
        /// <summary>
        /// Get the list of properties for this class T
        /// </summary>
        public PropertyInfo[] GetProperties()
        {
            return typeof(T).GetProperties();
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
                {
                    continue;
                }

                T item = new T();
                string[] lineItems = line.Split(',');

                for (int i = 0; i < lineItems.Length; i++)
                {
                    if (properties[i] == null)
                    {
                        continue;
                    }
                    PropertyInfo pinfo = properties[i];

                    string lineItem = lineItems[i];
                    pinfo.SetValue(item, Convert.ChangeType(lineItem, pinfo.PropertyType));
                }
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Import the data as the content of a csv file
        /// </summary>
        /// <param name="data">content of the csv, ',' separated</param>
        /// <param name="HeaderNameToPropertyNameLink">Links between header name and property name</param>
        /// <returns>List of converted items</returns>
        public List<T> ImportData(
            string data,
            Dictionary<string, string> HeaderNameToPropertyNameLink
        )
        {
            // Convert dictionary
            Dictionary<string, PropertyInfo> newLink = new Dictionary<string, PropertyInfo>();
            foreach (var kvpair in HeaderNameToPropertyNameLink)
            {
                newLink[kvpair.Key] = typeof(T).GetProperty(kvpair.Value);
            }

            return ImportData(data, newLink);
        }

        /// <summary>
        /// Import the data as the content of a csv file
        /// </summary>
        /// <param name="data">content of the csv, ',' separated</param>
        /// <param name="HeaderNameToPropertyLink">Links between header name and property</param>
        /// <returns>List of converted items</returns>
        public List<T> ImportData(
            string data,
            Dictionary<string, PropertyInfo> HeaderNameToPropertyLink,
            string separator = ","
        )
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            // Generate Properties Info if link is null
            if (HeaderNameToPropertyLink == null)
            {
                HeaderNameToPropertyLink = new Dictionary<string, PropertyInfo>();
                PropertyInfo[] properties = GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    HeaderNameToPropertyLink.Add(property.Name, property);
                }
            }

            // Ready to convert csv data...
            List<T> list = new List<T>();

            StringReader sr = new StringReader(data);
            using (TextFieldParser parser = new TextFieldParser(sr))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(separator);
                if (parser.EndOfData)
                    return null;

                string[] headers = parser.ReadFields();

                while (!parser.EndOfData)
                {
                    //Processing row
                    T newObject = new T();
                    string[] fields = parser.ReadFields();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        string field = fields[i];
                        string header = headers[i];
                        // Processing field
                        if (string.IsNullOrEmpty(field))
                        {
                            continue;
                        }

                        // If there's no link, do nothing
                        if (
                            !HeaderNameToPropertyLink.ContainsKey(header)
                            || HeaderNameToPropertyLink[header] == null
                        )
                        {
                            // No link for this header...
                            continue;
                        }

                        // Retrieve the link
                        PropertyInfo property = HeaderNameToPropertyLink[header];
                        // If value is not empty, apply it to the property
                        if (!string.IsNullOrEmpty(field))
                        {
                            property.SetValue(
                                newObject,
                                Convert.ChangeType(field, property.PropertyType)
                            );
                        }
                        else
                        {
                            var defaultValue =
                                (property.PropertyType.IsValueType)
                                    ? Activator.CreateInstance(property.PropertyType, true)
                                    : null;
                            property.SetValue(
                                newObject,
                                Convert.ChangeType(defaultValue, property.PropertyType)
                            );
                        }
                    }

                    list.Add(newObject);
                }
            }

            return list;
        }

        /// <summary>
        /// Ask for the user with a GUI way, to choose the headers linking
        /// </summary>
        public Dictionary<string, PropertyInfo> AskUserHeadersLinks(string data)
        {
            frmChooseHeaderLinking frm = new frmChooseHeaderLinking();
            frm.Populate<T>(data);
            frm.ShowDialog();
            if (frm.Result)
            {
                return frm.Output;
            }
            return null;
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
