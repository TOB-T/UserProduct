using System.Threading.Tasks;
using UserProductAPI.Core.DTOs;
using UserProductAPI.Core.Entities;

namespace UserProductAPI.Infrastructure.Interface
{
    public interface IProductRepository
    {

        Task<ProductResponseDto> AddProductAsync(ProductCreateDto productDto, string userId);
        Task<ProductResponseDto> UpdateProductAsync(ProductUpdateDto productDto, string userId);
        Task<ProductResponseDto> GetProductByIdAsync(int id, string userId);
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(string userId);
        Task<DeleteResponseDto> DeleteProductAsync(int id, string userId);
    }
}
