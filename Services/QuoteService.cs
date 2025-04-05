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
    public class QuoteService : IQuoteService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<QuoteService> _logger;

        public QuoteService(DatabaseContext dbContext, ILogger<QuoteService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<QuoteDTO>> GetAllQuotesAsync()
        {
            try
            {
                return await _dbContext.Quotes
                    .Select(q => new QuoteDTO
                    {
                        QuoteID = q.QuoteID,
                        RequestedDate = q.RequestedDate,
                        SupplierID = q.SupplierID,
                        QuoteStatus = q.QuoteStatus.ToString(),
                        RfqID = q.RfqID,
                        QuoteItems = q.QuoteItems.Select(qi => new QuoteItemDTO
                        {
                            QuoteItemID = qi.QuoteItemID,
                            ProductID = qi.ProductID,
                            RequestedQuantity = qi.RequestedQuantity,
                            QuotedPrice = qi.QuotedPrice
                        }).ToList()
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all quotes.");
                throw new Exception("An error occurred while retrieving quotes. Please try again later.");
            }
        }

        public async Task<QuoteDetailsDTO> GetQuoteByIdAsync(int id)
        {
            try
            {
                var quote = await _dbContext.Quotes
                    .Where(q => q.QuoteID == id)
                    .Select(q => new QuoteDetailsDTO
                    {
                        QuoteID = q.QuoteID,
                        RequestedDate = q.RequestedDate,
                        SupplierID = q.SupplierID,
                        QuoteStatus = q.QuoteStatus.ToString(),
                        RfqID = q.RfqID,
                        QuoteItems = q.QuoteItems.Select(qi => new QuoteItemDTO
                        {
                            QuoteItemID = qi.QuoteItemID,
                            ProductID = qi.ProductID,
                            RequestedQuantity = qi.RequestedQuantity,
                            QuotedPrice = qi.QuotedPrice
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (quote == null)
                {
                    _logger.LogWarning($"Quote with ID {id} not found.");
                    throw new KeyNotFoundException($"Quote with ID {id} does not exist.");
                }

                return quote;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the quote.");
                throw new Exception("An error occurred while retrieving the quote details. Please try again later.");
            }
        }

        public async Task<bool> CreateQuoteAsync(CreateQuoteDTO dto)
        {
            try
            {
                var quote = new Quote
                {
                    RequestedDate = dto.RequestedDate,
                    SupplierID = dto.SupplierID,
                    RfqID = dto.RfqID,
                    QuoteStatus = QuoteStatusEnum.Pending,
                    QuoteItems = dto.QuoteItems.Select(qi => new QuoteItem
                    {
                        ProductID = qi.ProductID,
                        RequestedQuantity = qi.RequestedQuantity,
                        QuotedPrice = qi.QuotedPrice
                    }).ToList()
                };

                _dbContext.Quotes.Add(quote);
                var result = await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Quote created successfully with ID {quote.QuoteID}.");
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the quote.");
                throw new ArgumentException("An error occurred while creating the quote. Please verify your inputs and try again.");
            }
        }

        public async Task<bool> UpdateQuoteAsync(int id, UpdateQuoteDTO dto)
        {
            try
            {
                var quote = await _dbContext.Quotes.FindAsync(id);
                if (quote == null)
                {
                    _logger.LogWarning($"Quote with ID {id} not found.");
                    throw new KeyNotFoundException($"Quote with ID {id} does not exist.");
                }

                quote.SupplierID = dto.SupplierID;
                quote.RequestedDate = dto.RequestedDate;

                var result = await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Quote with ID {id} updated successfully.");
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the quote with ID {id}.");
                throw new Exception("An error occurred while updating the quote. Please try again later.");
            }
        }

        public async Task<bool> UpdateQuoteStatusAsync(int id, QuoteStatusUpdateDTO statusDto)
        {
            try
            {
                var quote = await _dbContext.Quotes.FindAsync(id);
                if (quote == null)
                {
                    _logger.LogWarning($"Quote with ID {id} not found.");
                    throw new KeyNotFoundException($"Quote with ID {id} does not exist.");
                }

                quote.QuoteStatus = statusDto.QuoteStatus;
                var result = await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Quote status for ID {id} updated to {quote.QuoteStatus}.");
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the status for quote ID {id}.");
                throw new Exception("An error occurred while updating the quote status. Please try again later.");
            }
        }

        public async Task<bool> DeleteQuoteAsync(int id)
        {
            try
            {
                var quote = await _dbContext.Quotes.FindAsync(id);
                if (quote == null)
                {
                    _logger.LogWarning($"Quote with ID {id} not found.");
                    throw new KeyNotFoundException($"Quote with ID {id} does not exist.");
                }

                _dbContext.Quotes.Remove(quote);
                var result = await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Quote with ID {id} deleted successfully.");
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the quote with ID {id}.");
                throw new Exception("An error occurred while deleting the quote. Please try again later.");
            }
        }
    }
}
