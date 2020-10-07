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
            string ftpUsername = System.Configuration.ConfigurationManager.AppSettings["ftpUsername"];

            ServiceCSV CSV = new ServiceCSV();
            List<Products> DaneUni = CSV.CreateCSV();
            CSV.SaveCSV(DaneUni);

            //Console.ReadKey();
        }

    }
}
