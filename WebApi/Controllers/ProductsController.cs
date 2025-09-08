using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Entities;
using WebApi.Repositories;
using WebApi.Shared;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductsRepository _productsRepository;

        public ProductsController(ILogger<ProductsController> logger, ProductsRepository categoriesRepository)
        {
            _logger = logger;
            _productsRepository = categoriesRepository;
        }

        [HttpPost]
        public IActionResult Create(CreateProductDTO dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                SupplierID = dto.SupplierID,
                CategoryID = dto.CategoryID,
                QuantityPerUnit = dto.QuantityPerUnit,
                UnitPrice = dto.UnitPrice,
                UnitsInStock = dto.UnitsInStock,
                UnitsOnOrder = dto.UnitsOnOrder,
                ReorderLevel = dto.ReorderLevel,
                Discontinued = dto.Discontinued
            };

            this._productsRepository.CreateProduct(product);

            return CreatedAtRoute("GetProductById", new { id = product.ProductID }, product);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult GetById(int id)
        {
            var product = this._productsRepository.GetProduct(id);

            if (product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult List(
            [FromQuery] int? categoryID,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            if (pageNumber <= 0)
            {
                return BadRequest("PageNumber should be greater than 0");
            }

            if (pageSize <= 0)
            {
                return BadRequest("PageSize should be greater than 0");
            }

            ListProductsFilters? filters = categoryID == null
                    ? null
                    : new ListProductsFilters() { CategoryID = categoryID };

            PaginationParams pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

            var products = this._productsRepository.ListProducts(
                filters,
                pagination
            );

            return Ok(products);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateProductDTO dto)
        {
            var existingProduct = this._productsRepository.GetProduct(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            var product = new Product
            {
                ProductID = id,
                ProductName = dto.ProductName,
                SupplierID = dto.SupplierID,
                CategoryID = dto.CategoryID,
                QuantityPerUnit = dto.QuantityPerUnit,
                UnitPrice = dto.UnitPrice,
                UnitsInStock = dto.UnitsInStock,
                UnitsOnOrder = dto.UnitsOnOrder,
                ReorderLevel = dto.ReorderLevel,
                Discontinued = dto.Discontinued
            };

            this._productsRepository.UpdateProduct(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this._productsRepository.DeleteProduct(id);

            return NoContent();
        }
    }
}
