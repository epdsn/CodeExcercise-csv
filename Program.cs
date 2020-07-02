using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeExcerciseDataImportOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string fileLocation;
            string fileType; 
            string _numberOfFields;
            string[] fileData = null;

            Console.WriteLine("Where is the file located?");
            fileLocation = Console.ReadLine();

            Console.WriteLine("\r\nIs the file format CSV (comma-seperated values) or TSV (tab seperated values)?\r\n- Enter 1 for CSV Or Enter 2 for TSV:");
            fileType = Console.ReadLine();

            Console.WriteLine("\r\nHow many fields should each record contain?");
            _numberOfFields =  Console.ReadLine();

            try
            {
                fileData = File.ReadAllLines(@fileLocation);
            }
            catch( Exception ex ) {
                Console.WriteLine("\r\nThere was an error retrieving file.");
            }

            var dataFields = from row in fileData
                            let data = row.Split(',')
                            select new
                            {
                                FirstName = data[0],
                                MiddleName = data[1],
                                LastName = data[2]
                            };


            

            Console.WriteLine("output:\r\n--------------------------");
            var csv = new StringBuilder();
            foreach (var data in dataFields.Skip(1))
            {
                var newLine = $"{data.FirstName},{data.MiddleName},{data.LastName}";
                csv.AppendLine(newLine);

            }
            File.WriteAllText(@"newfile3.csv", csv.ToString());
            Console.WriteLine("--------------------------");

            Console.WriteLine("\r\nPress enter to exit");
            Console.ReadLine();
        }
    }
}
