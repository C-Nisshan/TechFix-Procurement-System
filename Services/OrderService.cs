using Backend_TechFix.Database;
using Backend_TechFix.DTOs;
using Backend_TechFix.Enums;
using Backend_TechFix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<OrderService> _logger;

        public OrderService(DatabaseContext dbContext, ILogger<OrderService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            try
            {
                return await _dbContext.Orders
                    .Select(o => new OrderDTO
                    {
                        OrderID = o.OrderID,
                        OrderDate = o.OrderDate,
                        SupplierID = o.SupplierID,
                        TotalAmount = o.TotalAmount,
                        OrderStatus = o.OrderStatus.ToString(),
                        QuoteID = o.QuoteID
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all orders.");
                throw new Exception("An error occurred while retrieving orders. Please try again later.");
            }
        }

        public async Task<OrderDetailsDTO> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _dbContext.Orders
                    .Where(o => o.OrderID == id)
                    .Select(o => new OrderDetailsDTO
                    {
                        OrderID = o.OrderID,
                        OrderDate = o.OrderDate,
                        SupplierID = o.SupplierID,
                        TotalAmount = o.TotalAmount,
                        OrderStatus = o.OrderStatus.ToString(),
                        QuoteID = o.QuoteID,
                        OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                        {
                            OrderItemID = oi.OrderItemID,
                            ProductID = oi.ProductID,
                            Quantity = oi.Quantity,
                            UnitPrice = oi.UnitPrice,
                            Discount = oi.Discount
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found.");
                    throw new KeyNotFoundException($"Order with ID {id} does not exist.");
                }

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the order.");
                throw new Exception("An error occurred while retrieving the order details. Please try again later.");
            }
        }

        public async Task<bool> CreateOrderAsync(CreateOrderDTO dto)
        {
            try
            {
                var quote = await _dbContext.Quotes.FindAsync(dto.QuoteID);
                if (quote == null)
                {
                    throw new ArgumentException("Invalid QuoteID provided.");
                }

                var order = new Order
                {
                    OrderDate = dto.OrderDate,
                    SupplierID = dto.SupplierID,
                    TotalAmount = dto.TotalAmount,
                    OrderStatus = OrderStatusEnum.Pending,
                    QuoteID = dto.QuoteID,
                    OrderItems = dto.OrderItems.Select(oi => new OrderItem
                    {
                        ProductID = oi.ProductID,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        Discount = oi.Discount
                    }).ToList()
                };

                _dbContext.Orders.Add(order);
                var result = await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Order created successfully with ID {order.OrderID}.");
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the order.");
                throw new ArgumentException("An error occurred while creating the order. Please verify your inputs and try again.");
            }
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, OrderStatusUpdateDTO statusDto)
        {
            try
            {
                var order = await _dbContext.Orders.FindAsync(id);
                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found.");
                    throw new KeyNotFoundException($"Order with ID {id} does not exist.");
                }

                order.OrderStatus = statusDto.OrderStatus;
                var result = await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Order status for ID {id} updated to {order.OrderStatus}.");
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the status for order ID {id}.");
                throw new Exception("An error occurred while updating the order status. Please try again later.");
            }
        }
    }
}
