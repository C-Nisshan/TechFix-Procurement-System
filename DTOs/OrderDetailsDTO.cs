namespace Backend_TechFix.DTOs
{
    public class OrderDetailsDTO : OrderDTO
    {
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
