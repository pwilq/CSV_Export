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
        public string Product_code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Products() { }

        public Products(string _product_code, string _name, string _desc)
        {
            this.Product_code = _product_code;
            this.Name = _name;
            this.Description = _desc;
        }
        
    }

}

