namespace Operations.Purchases
{
    public class PurchaseAggregateDTO
    {
        // Dimensiones
        public int Year { get; set; }
        public string SupplierName { get; set; } // Nombre del Proveedor
        public string Laboratory { get; set; } // Nombre del Laboratorio (de ProductoDIM)

        // Métricas
        public decimal TotalCostPurchase { get; set; } // Costo total de adquisición
        public int UnitsPurchased { get; set; } // Cantidad total comprada
    }
}