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


        public void Import()
        {

        }

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
                if(string.IsNullOrEmpty(line))
                { continue; }

                T item = new T();
                string[] lineItems = line.Split(',');

                for (int i = 0; i < lineItems.Length; i++)
                {
                    if (properties[i] == null) { continue; }

                    string lineItem = lineItems[i];
                    properties[i].SetValue(item, lineItem);
                }
                list.Add(item);
            }

            return list;
        }

    }
}
