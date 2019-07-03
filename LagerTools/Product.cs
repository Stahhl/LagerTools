using System;
using System.Collections.Generic;
using System.Text;

public enum Category
{
    NULL,
    test1,
    test2,
    test3,
    //TODO ADD MORE
}
public enum Storage
{
    NULL,
    test1,
    test2,
    test3,
}

namespace LagerTools
{
    public class Product
    {
        //produktnummer, produktkategori, produktnamn, plats
        public Product(string ProductNumber, Category ProductCategory, string ProductName, Storage ProductStorage)
        {
            this.ProductNumber = ProductNumber;
            this.ProductCategory = ProductCategory;
            this.ProductName = ProductName;
            this.ProductStorage = ProductStorage;
        }

        public string ProductNumber { get; private set; }
        public Category ProductCategory { get; private set; }
        public string ProductName { get; private set; }
        public Storage ProductStorage { get; private set; }
    }
}
