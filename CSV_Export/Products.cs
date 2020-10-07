using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace CSV_Export
{
    public class Products
    {
        public string product_code { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Products() { }

        public Products(string _product_code, string _name, string _desc)
        {
            this.product_code = _product_code;
            this.name = _name;
            this.description = _desc;
        } 
        
    }

}

