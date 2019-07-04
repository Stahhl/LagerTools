using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LagerTools
{
    public static class DbSearch
    {
        public static void SearchDb()
        {
            Console.Clear();
            Program.WriteLineColor("Mata in 'exit' för att gå tillbaka.", ConsoleColor.White);
            Program.WriteLineColor("Sök produkter efter: ", ConsoleColor.White);
            Program.WriteLineColor("All - A:term, Name - P:term, Number - N:term,  Catergory - C:term, Storage - S:term: ", ConsoleColor.White);

            string[] input = Program.GreenInput().Split(":");

            if (input.Length != 2)
                InvalidInput();

            switch(input[0].ToUpper())
            {
                case "A":
                    SearchDbAll(input[1]);
                    break;
                case "P":
                    SearchBy("name", input[1], false);
                    break;
                case "N":
                    SearchBy("number", input[1], false);
                    break;
                case "C":
                    SearchBy("category", input[1], false);
                    break;
                case "S":
                    SearchBy("storage", input[1], false);
                    break;
                default:
                    InvalidInput();
                    break;
            }
        }
        static void SearchDbAll(string term)
        {
            SearchBy("name", term, true);
            Console.WriteLine("-------------------------------------------------------------------------------");
            SearchBy("number", term, true);
            Console.WriteLine("-------------------------------------------------------------------------------");
            SearchBy("category", term, true);
            Console.WriteLine("-------------------------------------------------------------------------------");
            SearchBy("storage", term, true);

            Program.WriteColor("Press RETUR to go back: ", ConsoleColor.White);
            Console.ReadLine();
            SearchDb();
        }
        static void SearchBy(string searchBy, string term, bool keepGoing)
        {
            List<Product> hits = new List<Product>();
            term = term.ToUpper();

            switch(searchBy)
            {
                case "name":
                    hits = Program.productList.Where(x => x.ProductName.ToUpper().Contains(term)).ToList();
                    break;
                case "number":
                    hits = Program.productList.Where(x => x.ProductNumber.ToUpper().Contains(term)).ToList();
                    break;
                case "category":
                    hits = Program.productList.Where(x => x.ProductCategory.ToString().ToUpper().Contains(term)).ToList();
                    break;
                case "storage":
                    hits = Program.productList.Where(x => x.ProductStorage.ToString().ToUpper().Contains(term)).ToList();
                    break;
            }

            //var hits = Program.productList.Where(x => x.ProductName.Contains(term));
            Program.WriteLineColor($"Hits by {searchBy}: {hits.Count()}", ConsoleColor.White);
            Program.ListProducts(hits.ToList());
            if(keepGoing == false)
            {
                Program.WriteColor("Press RETUR to go back: ", ConsoleColor.White);
                Console.ReadLine();
                SearchDb();
            }
        }
        static void InvalidInput()
        {
            Console.Clear();
            Program.WriteLineColor("Invalid input ", ConsoleColor.Red);
            SearchDb();
        }
    }
}
