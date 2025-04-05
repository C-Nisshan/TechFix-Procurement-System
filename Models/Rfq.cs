namespace Backend_TechFix.Models
{
    public class Rfq
    {
        public int RfqID { get; set; }  // PK
        public DateTime RequestedDate { get; set; }
        public int TechFixUserID { get; set; }  // FK to User (TechFix)
        public int SupplierID { get; set; }  // FK to Supplier

        // Navigation Properties
        public User TechFixUser { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<RfqItem> RfqItems { get; set; }
    }
}
