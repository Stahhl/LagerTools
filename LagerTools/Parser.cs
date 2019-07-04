using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                );

            {

            };
            #endregion

            return product;
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
