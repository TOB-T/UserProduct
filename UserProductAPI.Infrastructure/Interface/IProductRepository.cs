using System.Threading.Tasks;
using UserProductAPI.Core.DTOs;
using UserProductAPI.Core.Entities;

namespace UserProductAPI.Infrastructure.Interface
{
    public interface IProductRepository
    {
        Task<ProductResponseDto> AddProductAsync(ProductCreateDto productDto);
        Task<ProductResponseDto> UpdateProductAsync(ProductUpdateDto productDto);
        Task<ProductResponseDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<DeleteResponseDto> DeleteProductAsync(int id);
    }
}
