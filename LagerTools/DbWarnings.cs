using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LagerTools
{
    public static class DbWarnings
    {
        //Instead of a bunch of error handling when creating products 
        //Use this class to present potential errors to the user
        public static void DisplayWarnings(List<Product> products)
        {
            Program.WriteLineColor("Possible errors in db: ", ConsoleColor.White);
            Console.WriteLine("-------------------------------------------------------------------------------");
            DuplicateNumbers(products);
            Console.WriteLine("-------------------------------------------------------------------------------");
            DuplicateNames(products);
            Console.WriteLine("-------------------------------------------------------------------------------");
            SameStorage(products);
            Console.WriteLine("-------------------------------------------------------------------------------");
            ProductsWithNullValues(products);
            Program.WriteColor("Press RETUR to go back: ", ConsoleColor.White);
            Console.ReadLine();
            Program.Main();
        }
        static void DuplicateNumbers(List<Product> products)
        {
            List<Product> productToDisplay = new List<Product>();

            var hits = from product in products
                       group product by product.ProductNumber into g
                       where g.Count() > 1
                       select g;

            foreach (var g in hits)
            {
                foreach (var item in g.ToList())
                {
                    productToDisplay.Add(item);
                }
            }
            Program.WriteLineColor("Products with duplicate product numbers: ", ConsoleColor.White);
            Program.ListProducts(productToDisplay);
        }
        static void DuplicateNames(List<Product> products)
        {
            List<Product> productToDisplay = new List<Product>();

            var hits = from product in products
                       group product by product.ProductName into g
                       where g.Count() > 1
                       select g;

            foreach (var g in hits)
            {
                foreach (var item in g.ToList())
                {
                    productToDisplay.Add(item);
                }
            }
            Program.WriteLineColor("Products with duplicate names: ", ConsoleColor.White);
            Program.ListProducts(productToDisplay);
        }
        static void SameStorage(List<Product> products)
        {
            List<Product> productToDisplay = new List<Product>();

            var hits = from product in products
                       group product by product.ProductStorage into g
                       where g.Count() > 1
                       select g;

            foreach (var g in hits)
            {
                foreach (var item in g.ToList())
                {
                    productToDisplay.Add(item);
                }
            }
            Program.WriteLineColor("Products on the same shelf: ", ConsoleColor.White);
            Program.ListProducts(productToDisplay);
        }
        static void ProductsWithNullValues(List<Product> products)
        {
            List<Product> productToDisplay = new List<Product>();

            var hits = (from p in products
                       where (
                       p.ProductNumber == "------" || 
                       p.ProductCategory == Category.NULL ||
                       p.ProductStorage == Storage.NULL )
                       select p).ToList();

            foreach (var g in hits)
            {
                productToDisplay.Add(g);
            }
            Program.WriteLineColor("Products with null values: ", ConsoleColor.White);
            Program.ListProducts(productToDisplay);
        }
    }
}
