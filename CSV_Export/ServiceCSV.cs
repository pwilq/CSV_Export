using CsvHelper;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            StringBuilder errorMessages = new StringBuilder();

            ConnectionStringSettingsCollection settings = System.Configuration.ConfigurationManager.ConnectionStrings;
            var ConnectionString = settings[1].ConnectionString;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    CreateSqlQuery();

                    SqlCommand queryCommand = new SqlCommand(this.SqlQuery, connection);
                    SqlDataReader reader = queryCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var outputHtml = dt.Rows[i][2].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>").Replace("</ b>", "</b>");
                            P.Add(new Products(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), outputHtml));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Brak danych, sprawdź zapytanie SQL.");
                    }
                    reader.Dispose();
                }
                catch (SqlException ex)
                {
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                }
            }
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

        public void CreateSqlQuery()
        {
            this.SqlQuery = "SELECT Twr_Kod AS product_code, " +
                            "(SELECT TLM_Tekst FROM CDN.Tlumaczenia WHERE(TLM_Numer = CDN.TwrKarty.Twr_GIDNumer) AND(TLM_Jezyk = 1668) AND(TLM_Typ = 16)) AS name, " +
                            "(SELECT TwO_Opis FROM CDN.TwrOpisy WHERE(TwO_TwrNumer = CDN.TwrKarty.Twr_GIDNumer) AND(TwO_Jezyk = 1668)) AS description " +
                            "FROM CDN.TwrKarty WHERE (Twr_Archiwalny <> 1) AND (Twr_ESklep = 1) ";
        }
    }
}
