using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//TODO Basic
//Felhantering - ok
//basic output
//Snygg output
// kolla längd på prod.nummer
//Todo Överkurs
//Filhatering
//Sök funktion

namespace LagerTools
{
    public class Program
    {
        static List<Product> readProducts;
        static List<Product> productList = new List<Product>();
        static void Main()
        {
            readProducts = Parser.ReadInventory("Inventory.csv");
            Console.WriteLine("Products in db: ");
            foreach (Product product in readProducts)
            {
                Console.WriteLine(product.ProductNumber + " " + product.ProductCategory + " " + product.ProductName + " " + product.ProductStorage);
            }

            Console.WriteLine(readProducts.Count());
            Console.WriteLine("LagerTools: ");
            while(AskForProducts())
            {

            }

            Console.WriteLine("Entered products: ");
            foreach (Product product in productList)
            {
                Console.WriteLine(product.ProductNumber + " " + product.ProductCategory + " " + product.ProductName + " " + product.ProductStorage);
            }
        }
        static bool AskForProducts()
        {
            Console.WriteLine("Mata in: produktnummer, produktkategori, produktnamn, plats: ");
            string[] input = Console.ReadLine().Split(",");

            for (int i = 0; i < input.Length; i++)
            {
                input[i] = input[i].Trim();
                //Console.WriteLine(input[i]);
                if (input[i].ToUpper() == "EXIT")
                    return false;
            }
            try
            {
                foreach (char c in input[0])
                {
                    int parsed = int.Parse(c.ToString());
                }

                productList.Add(
                    new Product
                    (
                    input[0],
                    Enum.TryParse(input[1], out Category category) ? category : Category.NULL,
                    input[2],
                    Enum.TryParse(input[3], out Storage storage) ? storage : Storage.NULL
                    ));
            }
            catch
            {
                //Console.WriteLine(e.Message);
                Console.WriteLine("Error: Invalid input ");
                Main();
            }
            Console.WriteLine("Number of products: " + productList.Count());
            return true;
        }
    }
}
