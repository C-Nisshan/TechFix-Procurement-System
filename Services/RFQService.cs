using Backend_TechFix.Database;
using Backend_TechFix.DTOs;
using Backend_TechFix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public class RFQService : IRFQService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<RFQService> _logger;

        public RFQService(DatabaseContext dbContext, ILogger<RFQService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<RFQDTO>> GetAllRFQsAsync()
        {
            try
            {
                return await _dbContext.Rfqs
                    .Select(r => new RFQDTO
                    {
                        RfqID = r.RfqID,
                        RequestedDate = r.RequestedDate,
                        TechFixUserID = r.TechFixUserID,
                        SupplierID = r.SupplierID,
                        RfqItems = r.RfqItems.Select(ri => new RFQItemDTO
                        {
                            RfqItemID = ri.RfqItemID,
                            ProductID = ri.ProductID,
                            RequestedQuantity = ri.RequestedQuantity
                        }).ToList() // Include the RfqItems
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all RFQs.");
                throw new Exception("An error occurred while retrieving RFQs. Please try again later.");
            }
        }

        public async Task<bool> CreateRFQAsync(CreateRFQDTO dto)
        {
            try
            {
                // Get all product IDs from the RFQ items
                var productIds = dto.RfqItems.Select(ri => ri.ProductID).ToList();

                // Check if all the products exist
                var productsExist = await _dbContext.Products
                    .Where(p => productIds.Contains(p.ProductID))
                    .CountAsync() == productIds.Count;

                if (!productsExist)
                {
                    _logger.LogWarning("One or more products in the RFQ do not exist.");
                    throw new ArgumentException("One or more products do not exist.");
                }


                var rfq = new Rfq
                {
                    RequestedDate = dto.RequestedDate,
                    TechFixUserID = dto.TechFixUserID,
                    SupplierID = dto.SupplierID,
                    RfqItems = dto.RfqItems.Select(ri => new RfqItem
                    {
                        ProductID = ri.ProductID,
                        RequestedQuantity = ri.RequestedQuantity
                    }).ToList()
                };

                _dbContext.Rfqs.Add(rfq);
                var result = await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"RFQ created successfully with ID {rfq.RfqID}.");
                return result > 0;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error creating RFQ: Invalid input data.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating RFQ.");
                throw new Exception("An error occurred while creating the RFQ. Please try again later.");
            }
        }

        public async Task<bool> UpdateRFQAsync(int id, UpdateRFQDTO dto)
        {
            try
            {
                var rfq = await _dbContext.Rfqs.Include(r => r.RfqItems).FirstOrDefaultAsync(r => r.RfqID == id);
                if (rfq == null)
                {
                    _logger.LogWarning($"RFQ with ID {id} not found.");
                    throw new KeyNotFoundException($"RFQ with ID {id} does not exist.");
                }

                rfq.RequestedDate = dto.RequestedDate;
                rfq.SupplierID = dto.SupplierID ?? rfq.SupplierID;

                if (dto.RfqItems != null)
                {
                    _dbContext.RfqItems.RemoveRange(rfq.RfqItems);
                    rfq.RfqItems = dto.RfqItems.Select(ri => new RfqItem
                    {
                        ProductID = ri.ProductID,
                        RequestedQuantity = ri.RequestedQuantity
                    }).ToList();
                }

                _dbContext.Rfqs.Update(rfq);
                var result = await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"RFQ with ID {id} updated successfully.");
                return result > 0;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Error updating RFQ with ID {id}: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while updating RFQ with ID {id}.");
                throw new Exception("An error occurred while updating the RFQ. Please try again later.");
            }
        }

        public async Task<bool> DeleteRFQAsync(int id)
        {
            try
            {
                var rfq = await _dbContext.Rfqs.FindAsync(id);
                if (rfq == null)
                {
                    _logger.LogWarning($"RFQ with ID {id} not found.");
                    throw new KeyNotFoundException($"RFQ with ID {id} does not exist.");
                }

                _dbContext.Rfqs.Remove(rfq);
                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    _logger.LogInformation($"RFQ with ID {id} successfully deleted.");
                    return true;
                }

                _logger.LogError($"Failed to delete RFQ with ID {id}. No changes were made.");
                throw new Exception("An error occurred while deleting the RFQ. Please try again later.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Error deleting RFQ with ID {id}: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while deleting RFQ with ID {id}.");
                throw new Exception("An error occurred while deleting the RFQ. Please try again later.");
            }
        }

        public async Task<RFQDetailsDTO> GetRFQByIdAsync(int id)
        {
            try
            {
                var rfq = await _dbContext.Rfqs
                    .Where(r => r.RfqID == id)
                    .Select(r => new RFQDetailsDTO
                    {
                        RfqID = r.RfqID,
                        RequestedDate = r.RequestedDate,
                        TechFixUserID = r.TechFixUserID,
                        SupplierID = r.SupplierID,
                        RfqItems = r.RfqItems.Select(ri => new RFQItemDTO
                        {
                            RfqItemID = ri.RfqItemID,
                            ProductID = ri.ProductID,
                            RequestedQuantity = ri.RequestedQuantity
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (rfq == null)
                {
                    _logger.LogWarning($"RFQ with ID {id} not found.");
                    throw new KeyNotFoundException($"RFQ with ID {id} does not exist.");
                }

                return rfq;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Error fetching RFQ with ID {id}: {ex.Message}");
                throw new Exception($"RFQ with ID {id} does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while fetching RFQ with ID {id}.");
                throw new Exception("An error occurred while fetching the RFQ details. Please try again later.");
            }
        }
    }
}
