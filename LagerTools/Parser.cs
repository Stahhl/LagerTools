using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace LagerTools
{
    static public class Parser
    {
        static public List<Product> ReadInventory(string dataFile)
        {
            //Read file on disk

            List<string> peopleRows = ReadFileToList(dataFile);

            // Remove first row

            RemoveFirstLine(peopleRows);

            // Create list of customers

            var productList = new List<Product>();
            
            foreach (var row in peopleRows)
            {

                if (string.IsNullOrWhiteSpace(row))
                {
                    continue;
                }

                Product product = ParseProductFromRow(row);

                productList.Add(product);
            }

            return productList;
        }
        static private Product ParseProductFromRow(string row)
        {
            string[] rowArray = row.Split(',');

            Product product = new Product
            #region constructor
                (
                    rowArray[1],
                    rowArray.Length >= 3 ? rowArray[2] : "------",
                    rowArray.Length >= 4 && Enum.TryParse(rowArray[3], true, out Category category) ? category : Category.NULL,
                    rowArray.Length >= 5 && Enum.TryParse(rowArray[4], true, out Storage storage) ? storage : Storage.NULL
                )
            {

            };
            #endregion

            return product;
        }
        public static void SaveToCsv(List<Product> products, string filePath)
        {
            if (File.Exists(filePath) == false)
                throw new Exception("FILEEEEEEEEEE");

            Program.productList.Clear();
            foreach (var p in products)
            {
                Program.productList.Add(p);
            }
            //Create a backup of previous db file
            //loop through the folder until a unique filename is found
            //copy the old db file with the new name
            string newPath = filePath;
            int count = 0;
            while (File.Exists(newPath))
            {
                string tempFileName = string.Format("{0}_{1}", filePath.Replace(".csv", ""), count++);
                newPath = tempFileName + ".csv";
            }
            Console.WriteLine("newPath = " + newPath);
            File.Copy(filePath, newPath);

            //Create a class just for writing to file
            List<WriteOutput> output = new List<WriteOutput>();
            for (int i = 0; i < products.Count(); i++)
            {
                output.Add(new WriteOutput()
                {
                    Id = i,
                    ProductName = products[i].ProductName,
                    ProductNumber = products[i].ProductNumber,
                    ProductCategory = products[i].ProductCategory.ToString(),
                    ProductStorage = products[i].ProductStorage.ToString(),
                }); ;
            }

            //Use extension 'csv helper' to write to file
            //it automatically writes the first row as the name of the class-properties
            //and fills in the values in the columns
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(output);
            }
        }
        static private void RemoveFirstLine(List<string> peopleRows)
        {
            peopleRows.RemoveAt(0);
        }
        static private List<string> ReadFileToList(string dataFileWithCustomers)
        {
            return File.ReadAllLines($"{dataFileWithCustomers}").ToList();
        }
    }
}
