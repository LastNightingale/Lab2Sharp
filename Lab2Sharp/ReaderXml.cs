using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Lab2Sharp
{
    internal class ReaderXml
    {
        public XDocument ReadFile(string filename)
        {
            if (filename == null) throw new NullReferenceException();
            return XDocument.Load(filename);
        }
    }
}
