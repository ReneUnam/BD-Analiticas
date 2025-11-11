namespace Operations.Sales
{
    public class SalesDTO
    {
        public int Year { get; set; }           
        public string Category { get; set; }    
     
        public decimal TotalSale { get; set; }  
        public int UnitsSold { get; set; }      
    }
}