using System;
using System.Collections.Generic;
using System.Linq;


//TODO Basic
//Felhantering - ok
//basic output - ok
//Snygg output - ok

//inga dubbletter
//-eller
//tillåt dubbletter men ha en funktion som visar 'fel' för användaren - ok

//TODO Överkurs
//Filhatering Läs - ok 
//Filhantering Skriv - ok
//Sök funktion - ok 

namespace LagerTools
{
    public class Program
    {
        static public List<Product> productList;
        static string fileName = "Data\\Inventory.csv";
        public static void Main()
        {
            if (productList == null)
                productList = Parser.ReadInventory(fileName);

            Console.Clear();
            WriteLineColor("Welcome to LagerTools TM! ", ConsoleColor.White);
            WriteLineColor("Products in inventory: " + productList.Count(), ConsoleColor.White);
            ChooseModule();
        }
        static void ChooseModule()
        {
            WriteColor("Choose Module: (A)dd products, (L)ist products, (S)earch products, (R)emove products, (W)arnings, (E)mpty shelves: ", ConsoleColor.White);
            string input = GreenInput().ToUpper();
            switch(input)
            {
                case "A":
                    AddProducts();
                    break;
                case "L":
                    #region case
                    Console.Clear();
                    WriteLineColor("Total products in inventory: " + productList.Count(), ConsoleColor.White);
                    ListProducts(productList);
                    WriteColor("Press RETUR to go back: ", ConsoleColor.White);
                    Console.ReadLine();
                    Main();
                    #endregion
                    break;
                case "S":
                    Console.Clear();
                    DbSearch.SearchDb();
                    break;
                case "R":
                    RemoveProduct(productList);
                    break;
                case "W":
                    Console.Clear();
                    DbWarnings.DisplayWarnings(productList);
                    break;
                case "E":
                    Console.Clear();
                    DbSearch.ShowEmptyShelves();
                    break;
                default:
                    WriteLineColor("Error Module not recognized: " + input, ConsoleColor.Red);
                    ChooseModule();
                    break;
            }
        }
        static void RemoveProduct(List<Product> products)
        {
            List<Product> tempList = new List<Product>();
            foreach (var item in products)
            {
                tempList.Add(item);
            }
            Console.Clear();
            WriteColor("Type 'exit' to go back without saving, 'save' to save and go back: ", ConsoleColor.White);
            WriteLineColor("Choose what products to remove by Id: ", ConsoleColor.White);
            ListProducts(tempList);
            WriteColor("Input: ", ConsoleColor.White);

            string input = GreenInput();
            if (input.ToUpper() == "EXIT")
                Main();
            if (input.ToUpper() == "SAVE")
            {
                WriteColor("Are you sure you want to overwrite? y/n: ", ConsoleColor.Red);
                if (GreenInput().ToUpper() == "Y")
                {
                    Program.productList.Clear();
                    foreach (var p in tempList)
                    {
                        Program.productList.Add(p);
                    }
                    Parser.SaveToCsv(Program.productList, fileName);
                    Main();
                }
            }

            if (int.TryParse(input, out int result) == true && result <= tempList.Count())
            {
                Console.Clear();
                WriteLineColor("Are you sure you want to remove? y/n: ", ConsoleColor.Red);
                ListProducts(new List<Product>() { tempList[result - 1] });
                if (GreenInput().ToUpper() == "Y")
                {
                    tempList.RemoveAt(result - 1);
                    RemoveProduct(tempList);
                }
            }
            else
            {
                Console.Clear();
                Program.WriteLineColor("Invalid input ", ConsoleColor.Red);
                RemoveProduct(products);
            }
        }
        public static void ListProducts(List<Product> products)
        {
            Console.WriteLine();
            WriteColor("Id" + "\t", ConsoleColor.White);
            WriteColor("ProductNumber" + "\t", ConsoleColor.White);
            WriteColor("ProductName".PadRight(25) + "\t", ConsoleColor.White);
            WriteColor("ProductCategory" + "\t", ConsoleColor.White);
            WriteColor("ProductStorage" + "\t", ConsoleColor.White);
            Console.WriteLine();
            int index = 1;
            foreach (Product product in products)
            {
                WriteColor(index + "\t", ConsoleColor.White);
                index++;
                WriteColor(product.ProductNumber.PadRight(15) + "\t", Product.NumberColor);
                WriteColor(product.ProductName.PadRight(25) + "\t", Product.NameColor);
                WriteColor(product.ProductCategory.ToString().PadRight(15) + "\t", Product.CategoryColor);
                WriteColor(product.ProductStorage + "\t", Product.StorageColor);
                Console.WriteLine();
            }
            Console.WriteLine();

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
            WriteLineColor("Enter 'exit' to exit without saveing, 'save' to save the new list", ConsoleColor.White);
            WriteLineColor("Mata in: produktnamn, produktnummer, produktkategori, lagerplats: ", ConsoleColor.White);

            string[] input = GreenInput().Split(",");

            for (int i = 0; i < input.Length; i++)
            {
                input[i] = input[i].Trim();
                if (input[i].ToUpper() == "EXIT")
                    return false;
                if (input[i].ToUpper() == "SAVE")
                {
                    Parser.SaveToCsv(productList, fileName);
                    return false;
                }
            }
            try
            {
                bool parsed = true;
                if (input[0] == string.Empty)
                    throw new Exception();

                if(input.Length >= 2)
                {
                    if (input[1] == string.Empty)
                        parsed = false;
                    foreach (char c in input[1])
                        if (int.TryParse(c.ToString(), out int x) == false)
                            parsed = false;
                }


                productList.Add(
                    new Product
                    (
                    input[0],
                    input.Length >= 2 && parsed == true ? input[1] : "------",
                    input.Length >= 3 && Enum.TryParse(input[2], true, out Category category) ? category : Category.NULL,
                    input.Length >= 4 && Enum.TryParse(input[3], true, out Storage storage) ? storage : Storage.NULL
                    ));
            }
            catch
            {
                Console.Clear();
                WriteLineColor("Error Invalid input - Product must have a name", ConsoleColor.Red);
                return true;
            }
            Console.Clear();
            WriteLineColor("Product added succesfully! ", ConsoleColor.Yellow);
            return true;
        }
        public static void WriteColor(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ResetColor();
        }
        public static void WriteLineColor(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        public static string GreenInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }
    }
}
