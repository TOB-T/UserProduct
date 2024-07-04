using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProductAPI.Core.DTOs;
using UserProductAPI.Infrastructure.Interface;

namespace UserProductAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ITokenInterface _tokenService;

        public ProductController(IProductRepository productRepository, ITokenInterface tokenService)
        {
            _productRepository = productRepository;
            _tokenService = tokenService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            var userId = GetUserId();
            var result = await _productRepository.AddProductAsync(productDto, userId);
            return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto productDto)
        {
            var userId = GetUserId();
            var result = await _productRepository.UpdateProductAsync(productDto, userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(int id)
        {
            var userId = GetUserId();
            var result = await _productRepository.GetProductByIdAsync(id, userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            var userId = GetUserId();
            var result = await _productRepository.GetAllProductsAsync(userId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userId = GetUserId();
            var result = await _productRepository.DeleteProductAsync(id, userId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }
    }
}



