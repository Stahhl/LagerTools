namespace LagerTools
{
    //Only used for writing to csv
    public class WriteOutput
    {
        //Unique incremental number for saving to csv
        public int Id { get; set; }
        //Properties of product class
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string ProductCategory { get; set; }
        public string ProductStorage { get; set; }
    }
}
