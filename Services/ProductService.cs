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
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<ProductService> _logger;

        public ProductService(DatabaseContext dbContext, ILogger<ProductService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            return await _dbContext.Products
                .Select(p => new ProductDTO
                {
                    ProductID = p.ProductID,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryID = p.CategoryID,
                    BrandID = p.BrandID,
                    ModelNumber = p.ModelNumber
                })
                .ToListAsync();
        }

        public async Task<bool> CreateProductAsync(CreateProductDTO dto)
        {
            // Check if Category and Brand exist
            var categoryExists = await _dbContext.Categories.AnyAsync(c => c.CategoryID == dto.CategoryID);
            if (!categoryExists)
                throw new ArgumentException("Invalid CategoryID. The specified category does not exist.");

            var brandExists = await _dbContext.Brands.AnyAsync(b => b.BrandID == dto.BrandID);
            if (!brandExists)
                throw new ArgumentException("Invalid BrandID. The specified brand does not exist.");

            // Check for unique combination of BrandID and ModelNumber
            var existingProduct = await _dbContext.Products
                .AnyAsync(p => p.BrandID == dto.BrandID && p.ModelNumber == dto.ModelNumber);
            if (existingProduct)
                throw new ArgumentException("A product with the same BrandID and ModelNumber already exists.");

            // Create Product
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                CategoryID = dto.CategoryID,
                BrandID = dto.BrandID,
                ModelNumber = dto.ModelNumber
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();  // Save the Product and generate ProductID

            // Create associated SupplierProduct entries
            if (dto.SupplierProducts != null)
            {
                var supplierProducts = dto.SupplierProducts.Select(sp => new SupplierProduct
                {
                    ProductID = product.ProductID,  // Set the generated ProductID
                    SupplierID = sp.SupplierID,
                    UnitPrice = sp.UnitPrice,
                    Discount = sp.Discount
                }).ToList();

                _dbContext.SupplierProducts.AddRange(supplierProducts);
            }

            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDTO dto)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null) return false;

            // Check for unique combination of BrandID and ModelNumber if updated
            if (!string.IsNullOrEmpty(dto.ModelNumber) && dto.BrandID.HasValue)
            {
                var existingProduct = await _dbContext.Products
                    .AnyAsync(p => p.BrandID == dto.BrandID && p.ModelNumber == dto.ModelNumber && p.ProductID != id);
                if (existingProduct)
                    throw new ArgumentException("A product with the same BrandID and ModelNumber already exists.");
            }

            // Update product properties
            product.Name = dto.Name ?? product.Name;
            product.Description = dto.Description ?? product.Description;
            product.CategoryID = dto.CategoryID ?? product.CategoryID;
            product.BrandID = dto.BrandID ?? product.BrandID;
            product.ModelNumber = dto.ModelNumber ?? product.ModelNumber;

            // Update SupplierProduct entries
            if (dto.SupplierProducts != null)
            {
                var existingSupplierProducts = _dbContext.SupplierProducts.Where(sp => sp.ProductID == product.ProductID);
                _dbContext.SupplierProducts.RemoveRange(existingSupplierProducts); // Clear old entries

                var newSupplierProducts = dto.SupplierProducts.Select(sp => new SupplierProduct
                {
                    ProductID = product.ProductID,
                    SupplierID = sp.SupplierID ?? 0,
                    UnitPrice = sp.UnitPrice ?? 0m,
                    Discount = sp.Discount ?? 0m
                }).ToList();

                _dbContext.SupplierProducts.AddRange(newSupplierProducts); // Add new entries
            }

            _dbContext.Products.Update(product);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products.FindAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    throw new KeyNotFoundException($"Product with ID {id} does not exist.");
                }

                var supplierProducts = _dbContext.SupplierProducts.Where(sp => sp.ProductID == id);
                if (supplierProducts.Any())
                {
                    _dbContext.SupplierProducts.RemoveRange(supplierProducts);
                }

                _dbContext.Products.Remove(product);

                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    _logger.LogInformation($"Product with ID {id} successfully deleted.");
                    return true;
                }

                _logger.LogError($"Failed to delete product with ID {id}. No changes were made.");
                throw new Exception("An error occurred while deleting the product. Please try again later.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID {id}: {ex.Message}");
                throw new Exception($"Product with ID {id} does not exist and cannot be deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while deleting product with ID {id}.");
                throw new Exception("An error occurred while deleting the product. Please try again later.");
            }
        }

        public async Task<ProductDetailsDTO> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products
                    .Where(p => p.ProductID == id)
                    .Select(p => new ProductDetailsDTO
                    {
                        ProductID = p.ProductID,
                        Name = p.Name,
                        Description = p.Description,
                        CategoryID = p.CategoryID,
                        BrandID = p.BrandID,
                        ModelNumber = p.ModelNumber,
                        SupplierProducts = p.SupplierProducts
                            .Select(sp => new CreateSupplierProductDTO
                            {
                                SupplierID = sp.SupplierID,
                                ProductID = sp.ProductID,
                                UnitPrice = sp.UnitPrice,
                                Discount = sp.Discount
                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    throw new KeyNotFoundException($"Product with ID {id} does not exist.");
                }

                return product;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Error fetching product with ID {id}: {ex.Message}");
                throw new Exception($"Product with ID {id} does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while fetching product with ID {id}.");
                throw new Exception("An error occurred while fetching the product details. Please try again later.");
            }
        }
    }
}
