using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserProductAPI.Core.DTOs;
using UserProductAPI.Core.Entities;
using UserProductAPI.Infrastructure.Data;
using UserProductAPI.Infrastructure.Interface;

namespace UserProductAPI.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly UserProductDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(UserProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> AddProductAsync(ProductCreateDto productDto, string userId)
        {
            var product = _mapper.Map<Product>(productDto);
            product.UserId = userId;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> UpdateProductAsync(ProductUpdateDto productDto, string userId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productDto.Id && p.UserId == userId);
            if (product == null)
            {
                return null;
            }

            // Only update non-ID fields
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id, string userId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(string userId)
        {
            var products = await _context.Products.Where(p => p.UserId == userId).ToListAsync();
            return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        }

        public async Task<DeleteResponseDto> DeleteProductAsync(int id, string userId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (product == null)
            {
                return new DeleteResponseDto { Success = false, Message = "Product not found." };
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new DeleteResponseDto { Success = true, Message = "Product deleted successfully." };
        }
    }
}









