using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab2Sharp
{
    internal class WriterXml
    {
        public void WriteFile<T>(string filename, List<T> list) where T : class
        {
            using (XmlWriter filewriter = XmlWriter.Create(filename))
            {
                filewriter.WriteStartDocument();
                filewriter.WriteStartElement($"{list.First().GetType().Name}s");
                filewriter.WriteEndElement();
                filewriter.WriteEndDocument();
            }
            FillFileWithData(filename, list);
        }
        public void FillFileWithData<T>(string filename, List<T> list) where T : class
        {
            foreach (T item in list)
            {
                AddElement(filename, item);
            }
        }
        public void AddElement<T>(string filename, T element)
        {
            Type type = element.GetType();
            var allproperties = element.GetType().GetProperties();
            XElement[] items = new XElement[allproperties.Length];
            for (int i = 0; i < allproperties.Length; i++)
            {
                items[i] = new XElement(allproperties[i].Name, allproperties[i].GetValue(element));
            }
            XElement newElement = new XElement(type.Name, items);
            XDocument xDoc;
            xDoc = XDocument.Load(filename);
            xDoc.Root.Add(newElement);
            xDoc.Save(filename);
        }
    }
}
