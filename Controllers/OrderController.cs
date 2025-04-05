using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_TechFix.Services;
using Backend_TechFix.DTOs;
using System;
using System.Threading.Tasks;

namespace Backend_TechFix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [Authorize(Policy = "CanViewOrdersPolicy")]
        [HttpGet("get-all-orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orders.");
                return StatusCode(500, "An error occurred while retrieving orders.");
            }
        }

        [Authorize(Policy = "CanViewOrdersPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Order with ID {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving order with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the order.");
            }
        }

        [Authorize(Policy = "CanCreateOrderPolicy")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO orderDto)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(orderDto);
                if (!result)
                {
                    return BadRequest("Failed to create order.");
                }
                return Ok("Order created successfully.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid data provided for order creation.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order.");
                return StatusCode(500, "An error occurred while creating the order.");
            }
        }

        [Authorize(Policy = "CanUpdateOrderStatusPolicy")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDTO statusDto)
        {
            try
            {
                var result = await _orderService.UpdateOrderStatusAsync(id, statusDto);
                if (!result)
                {
                    return NotFound("Order not found or status update failed.");
                }
                return Ok("Order status updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Order with ID {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for order with ID {id}.");
                return StatusCode(500, "An error occurred while updating the order status.");
            }
        }
    }
}
