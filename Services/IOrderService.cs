using Backend_TechFix.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDetailsDTO> GetOrderByIdAsync(int id);
        Task<bool> CreateOrderAsync(CreateOrderDTO dto);
        Task<bool> UpdateOrderStatusAsync(int id, OrderStatusUpdateDTO statusDto);
    }
}
