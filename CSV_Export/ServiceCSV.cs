using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Export
{
    class ServiceCSV
    {
        public string SqlQuery { get; set; }
        public string ConnectionString { get; private set; }

        List<Products> P = new List<Products>();


        public List<Products> CreateCSV()
        {
            P.Add(new Products("AAA", "AAAname", "AAAdesc"));
            P.Add(new Products("BBB", "BBBname", "BBBdesc"));
            return P;
        }

        public void SaveCSV(List<Products> productList)
        {
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
            {
                csvWriter.Configuration.Delimiter = ";";
                csvWriter.Configuration.HasHeaderRecord = true;
                csvWriter.Configuration.AutoMap<Products>();

                csvWriter.WriteHeader<Products>();
                csvWriter.NextRecord();
                csvWriter.WriteRecords(productList);

                writer.Flush();
                var result = Encoding.UTF8.GetString(mem.ToArray());
                Console.WriteLine(result);
                
                File.WriteAllText(@"d:\test.csv", result.ToString());
                
            }
        }



    }

     
}
