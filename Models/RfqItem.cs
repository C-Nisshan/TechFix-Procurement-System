namespace Backend_TechFix.Models
{
    public class RfqItem
    {
        public int RfqItemID { get; set; }  // PK
        public int RfqID { get; set; }  // FK
        public int ProductID { get; set; }  // FK
        public int RequestedQuantity { get; set; }

        // Navigation Properties
        public Rfq Rfq { get; set; }
        public Product Product { get; set; }
    }
}
