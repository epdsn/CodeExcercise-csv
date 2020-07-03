using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeExcerciseDataImportOutput
{
    class Program
    {
        private static void Main(string[] args)
        {
            
            string fileLocation;
            int numberOfFields;
            string[] fileData = null;
            string delimiter;
            ConsoleKeyInfo cki;

            Console.WriteLine("--------------------------------");
            Console.WriteLine(" CSV and TSV data exporter");
            Console.WriteLine("--------------------------------");

            int count = 0;
            do
            {
                if (count > 0) {
                    Console.WriteLine("\r\nFile does not exist OR you do not sufficant permissions.");
                    Console.WriteLine("\rPlease enter a valid file path:");
                }
                count++;
                Console.WriteLine("\r\nWhere is the file located?");
                fileLocation = Console.ReadLine();
            } while (!File.Exists(fileLocation));

            Console.WriteLine("\r\nIs the file format CSV (comma-seperated values) or TSV (tab seperated values)?");
            Console.WriteLine("Enter 1 for CSV Or Enter 2 for TSV:");
            delimiter = (Convert.ToInt32(Console.ReadLine()) == 1) ? "," : "\t";

            Console.WriteLine("\r\nHow many fields should each record contain?");
            numberOfFields = Convert.ToInt32(Console.ReadLine());

            fileData = readData(@fileLocation);
            ReadRecords(fileData, delimiter, numberOfFields );

            Console.WriteLine("\r\nExport Complete");
            Console.WriteLine("\nPress the \"q\" key to quit.");
            do
            {
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Q);

        }

        public static string[] readData(string filepath)
        {
            try
            {
                return File.ReadAllLines(@filepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error reading the file.");
                throw new ApplicationException("Error retrieving record:", ex);
            }
        }

        public static void ReadRecords(string[] data, string delimiter, int numOfFields)
        {

            try
            {
                var header = data[0].Split(delimiter);
                var correctFields = new StringBuilder();
                var incorrectFields = new StringBuilder();

                foreach (var row in data.Skip(1))
                {
                    string[] recordFields = row.Split(delimiter);

                    if ((recordFields.Count()) == numOfFields)
                    {
                        correctFields.AppendLine(row);
                    }
                    else {
                        incorrectFields.AppendLine(row);
                    }
                }
                File.WriteAllText(@"Records.csv", correctFields.ToString());
                File.WriteAllText(@"InvalidFieldRecords.csv", incorrectFields.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error reading the record.");
                throw new ApplicationException("Error retrieving record:", ex);
            }

        }

    }
}
