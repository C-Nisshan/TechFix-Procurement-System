using Backend_TechFix.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public interface IQuoteService
    {
        Task<IEnumerable<QuoteDTO>> GetAllQuotesAsync();
        Task<QuoteDetailsDTO> GetQuoteByIdAsync(int id);
        Task<bool> CreateQuoteAsync(CreateQuoteDTO dto);
        Task<bool> UpdateQuoteAsync(int id, UpdateQuoteDTO dto);
        Task<bool> UpdateQuoteStatusAsync(int id, QuoteStatusUpdateDTO statusDto);
        Task<bool> DeleteQuoteAsync(int id);
    }
}
