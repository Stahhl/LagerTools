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
            List<string> peopleRows = ReadFileToList(dataFile);

            // Remove first line

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
                    Enum.TryParse(rowArray[2], out Category category) ? category : Category.NULL,
                    rowArray[3],
                    Enum.TryParse(rowArray[4], out Storage storage) ? storage : Storage.NULL
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
            //return File.ReadAllLines($"Linq\\Data\\{dataFileWithCustomers}").ToList();
            return File.ReadAllLines($"{dataFileWithCustomers}").ToList();
        }
    }
}
