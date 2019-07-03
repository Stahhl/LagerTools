using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//TODO Basic
//Felhantering - ok
//basic output
//Snygg output
// kolla längd på prod.nummer
//inge dubbleter artno

//TODO Överkurs
//Filhatering
//Sök funktion
//inga dubbleter lagerplats

namespace LagerTools
{
    public class Program
    {
        //static List<Product> readProducts;
        static List<Product> productList;
        static void Main()
        {
            if (productList == null)
                productList = Parser.ReadInventory("Inventory.csv");

            Console.Clear();
            WriteLineColor("Welcome to LagerTools TM! ", ConsoleColor.White);
            WriteLineColor("Products in inventory: " + productList.Count(), ConsoleColor.White);
            ChooseModule();
        }
        static void ChooseModule()
        {
            WriteColor("Choose Module: (A)dd products, (L)ist products, (S)earch products: ", ConsoleColor.White);
            string input = GreenInput().ToUpper();
            switch(input)
            {
                case "A":
                    AddProducts();
                    break;
                case "L":
                    ListProducts();
                    break;
                case "S":
                    SearchProducts();
                    break;
                default:
                    WriteLineColor("Error Module not recognized: " + input, ConsoleColor.Red);
                    ChooseModule();
                    break;
            }
        }

        private static void SearchProducts()
        {
            throw new NotImplementedException();
        }

        private static void ListProducts()
        {
            Console.Clear();
            WriteLineColor("Products in inventory: " + productList.Count(), ConsoleColor.White);
            Console.WriteLine();
            WriteColor("ProductNumber" + "\t", ConsoleColor.White);
            WriteColor("ProductName" + "\t", ConsoleColor.White);
            WriteColor("ProductCategory" + "\t".PadRight(9), ConsoleColor.White);
            WriteColor("ProductStorage" + "\t", ConsoleColor.White);
            Console.WriteLine();
            foreach (Product product in productList)
            {
                WriteColor(product.ProductNumber.PadRight(15) + "\t", ConsoleColor.Blue);
                WriteColor(product.ProductName.PadRight(15) + "\t", ConsoleColor.Green);
                WriteColor(product.ProductCategory.ToString().PadRight(20) + "\t", ConsoleColor.Yellow);
                WriteColor(product.ProductStorage + "\t", ConsoleColor.Cyan);
                Console.WriteLine();
            }
            Console.WriteLine();
            WriteColor("Press RETUR to go back: ", ConsoleColor.White);
            Console.ReadLine();
            Main();
        }

        static void AddProducts()
        {
            Console.Clear();
            while (AskForProducts())
            {

            }
            Main();
        }
        static bool AskForProducts()
        {
            WriteLineColor("Mata in 'exit' för att gå tillbaka.", ConsoleColor.White);
            WriteLineColor("Mata in: produktnummer, produktkategori, produktnamn, plats: ", ConsoleColor.White);

            string[] input = GreenInput().Split(",");

            for (int i = 0; i < input.Length; i++)
            {
                input[i] = input[i].Trim();
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
                Console.Clear();
                WriteLineColor("Error Invalid input! ", ConsoleColor.Red);
                return true;
            }
            Console.Clear();
            WriteLineColor("Product added succesfully! ", ConsoleColor.Yellow);
            return true;
        }
        static void WriteColor(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ResetColor();
        }
        static void WriteLineColor(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        static string GreenInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }
    }
}
