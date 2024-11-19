using HelpStockApp.Application.DTOs;
using HelpStockApp.Application.Interfaces;
using HelpStockApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpStockApp.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products == null)
            {
                return NotFound("Categories not found");
            }

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Invalid request Data");
            }
        
            await _productService.Add(productDTO);

            return Ok("Created");
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Invalid request Data");
            }

            await _productService.Update(productDTO);

            return Ok("Updated");
        }

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        public async Task<ActionResult> Remove(int? id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not Found");
            }

            await _productService.Remove(id);

            return Ok("Removed");
        }
    }
}
