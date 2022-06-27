using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Lab2Sharp
{
    internal class Printer
    {        
        public void Prints<TValue>(IEnumerable<TValue> toPrint)
        {
            foreach (TValue el in toPrint)
            {
                Console.WriteLine(el);
            }
        }
        public void Prints(string filename)
        {
            XmlDocument xDoc = new XmlDocument();
            if (!File.Exists(filename)) return;
            xDoc.Load(filename);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode node in xRoot.ChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    Console.Write($"{childNode.Name}: {childNode.InnerText}, ");
                }
                Console.WriteLine(" ");
            }
        }

        public void Prints<T,Q>(Dictionary <T, List<Q>> dictionary)
        {
            foreach (var groupQuere in dictionary)
            {
                Console.WriteLine(groupQuere.Key);
                foreach (var el in groupQuere.Value)
                {
                    Console.WriteLine(el.ToString());
                }
            }
        }
    }
}
