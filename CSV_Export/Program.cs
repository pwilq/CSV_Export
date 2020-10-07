using System;
using System.Configuration;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace CSV_Export
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ServiceCSV CSV = new ServiceCSV();
            List<Products> DataToCsv = CSV.CreateCSV();
            CSV.SaveCSV(DataToCsv);
        }

    }
}
