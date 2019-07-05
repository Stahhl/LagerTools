using System;
using System.Collections.Generic;
using System.Linq;

namespace LagerTools
{
    //Handle searching db by various terms
    public static class DbSearch
    {
        //categories to search by
        private enum SearchBy
        {
            NULL,
            Name,
            Number,
            Category,
            Storage,
        }
        public static void SearchDb()
        {
            Program.WriteLineColor("Mata in 'exit' för att gå tillbaka.", ConsoleColor.White);
            Program.WriteLineColor("Sök produkter efter: ", ConsoleColor.White);
            Program.WriteLineColor("All - A:term, Name - P:term, Number - N:term,  Catergory - C:term, Storage - S:term: ", ConsoleColor.White);

            string[] input = Program.GreenInput().Split(":");

            if(input[0].ToUpper() == "EXIT")
                Program.Main();

            if (input.Length != 2)
                InvalidInput();

            switch(input[0].ToUpper())
            {
                case "A":
                    SearchDbAll(input[1]);
                    break;
                case "P":
                    SearchDb(SearchBy.Name, input[1], false);
                    break;
                case "N":
                    SearchDb(SearchBy.Number, input[1], false);
                    break;
                case "C":
                    SearchDb(SearchBy.Category, input[1], false);
                    break;
                case "S":
                    SearchDb(SearchBy.Storage, input[1], false);
                    break;
                default:
                    InvalidInput();
                    break;
            }
        }
        static void SearchDbAll(string term)
        {
            SearchDb(SearchBy.Name, term, true);
            Console.WriteLine("-------------------------------------------------------------------------------");
            SearchDb(SearchBy.Number, term, true);
            Console.WriteLine("-------------------------------------------------------------------------------");
            SearchDb(SearchBy.Category, term, true);
            Console.WriteLine("-------------------------------------------------------------------------------");
            SearchDb(SearchBy.Storage, term, true);

            Program.WriteColor("Press RETUR to go back: ", ConsoleColor.White);
            Console.ReadLine();
            SearchDb();
        }
        static void SearchDb(SearchBy searchBy, string term, bool keepGoing)
        {
            List<Product> hits = new List<Product>();
            term = term.ToUpper();

            //Do a linq query depending on 'searchBy' parameter
            //list items in query
            switch (searchBy)
            {
                case SearchBy.Name:
                    hits = Program.productList.Where(x => x.ProductName.ToUpper().Contains(term)).ToList();
                    break;
                case SearchBy.Number:
                    hits = Program.productList.Where(x => x.ProductNumber.ToUpper().Contains(term)).ToList();
                    break;
                case SearchBy.Category:
                    hits = Program.productList.Where(x => x.ProductCategory.ToString().ToUpper().Contains(term)).ToList();
                    break;
                case SearchBy.Storage:
                    hits = Program.productList.Where(x => x.ProductStorage.ToString().ToUpper().Contains(term)).ToList();
                    break;
            }

            Program.WriteLineColor($"Hits by {searchBy}: {hits.Count()}", ConsoleColor.White);
            Program.ListProducts(hits.ToList());
            if(keepGoing == false)
            {
                Program.WriteColor("Press RETUR to go back: ", ConsoleColor.White);
                Console.ReadLine();
                SearchDb();
            }
        }
        internal static void ShowEmptyShelves()
        {
            //(9 * 26) + 2 = 236 total enum values
            //TODO: Refract 3 to 1 query (?!)
            var allEnums = Enum.GetValues(typeof(Storage)).Cast<Storage>().ToList();
            var usedEnums = Program.productList.Select(p => p.ProductStorage).Distinct().ToList();
            var unUsedEnums = allEnums.Where(x => usedEnums.Contains(x) == false).ToList();

            Program.WriteLineColor("Empty Shelves: " + unUsedEnums.Count(), ConsoleColor.White);
            Console.WriteLine();

            //Output empty files 12 per row and max 999 columns
            //break if there are no more values to print
            int index = 0;
            for (int a = 0; a < 999; a++)
            {
                if (index >= unUsedEnums.Count())
                    break;

                for (int b = 0; b < 12; b++)
                {
                    if (index >= unUsedEnums.Count())
                        break;

                    Program.WriteColor(unUsedEnums[index] + " ", Product.StorageColor);
                    index++;
                }
                Console.WriteLine();
            }
            Program.WriteColor("\nPress RETUR to go back: ", ConsoleColor.White);
            Console.ReadLine();
            Program.Main();
        }
        public static void InvalidInput()
        {
            Console.Clear();
            Program.WriteLineColor("Invalid input ", ConsoleColor.Red);
            SearchDb();
        }
    }
}
