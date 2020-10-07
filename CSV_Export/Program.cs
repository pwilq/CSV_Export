using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSV_Export
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCSV CSV = new ServiceCSV();
            List<Products> DaneUni = CSV.CreateCSV();
            CSV.SaveCSV(DaneUni);
            Console.ReadKey();
        }
    }s
}
