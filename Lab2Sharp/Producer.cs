using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Sharp
{
    internal class Producer
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public override string ToString()
        {
            string res = $"CountryID:{CountryID}, CountryName:{CountryName}" + "\n";            
            return res;
        }
    }
}
