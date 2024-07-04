using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        [Authorize]
        //[AllowAnonymous]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            var result = await _productRepository.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
        }

        [HttpPut]
        [Authorize]
        //[AllowAnonymous]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto productDto)
        {
            var result = await _productRepository.UpdateProductAsync(productDto);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productRepository.GetProductByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productRepository.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        //[AllowAnonymous]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProductAsync(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }
    }
}
