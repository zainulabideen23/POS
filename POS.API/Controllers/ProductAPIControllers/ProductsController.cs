using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Product.Application.Dtos.Products;
using POS.Product.Application.Interfaces.ProductInterfaces;

namespace POS.API.Controllers.ProductAPIControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(
            IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            return Ok(
                await _productService.GetAllAsync(
                    cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var product =
                await _productService.GetByIdAsync(
                    id,
                    cancellationToken);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("{id:guid}/details")]
        public async Task<IActionResult> GetDetails(
            Guid id,
            CancellationToken cancellationToken)
        {
            var product =
                await _productService.GetDetailsAsync(
                    id,
                    cancellationToken);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("{id:guid}/variants")]
        public async Task<IActionResult> GetVariants(
            Guid id,
            CancellationToken cancellationToken)
        {
            var variants =
                await _productService.GetVariantsAsync(
                    id,
                    cancellationToken);

            return Ok(variants);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductDto dto,
            CancellationToken cancellationToken)
        {
            var product =
                await _productService.CreateAsync(
                    dto,
                    cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateProductDto dto,
            CancellationToken cancellationToken)
        {
            var product =
                await _productService.UpdateAsync(
                    id,
                    dto,
                    cancellationToken);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result =
                await _productService.DeleteAsync(
                    id,
                    cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> Activate(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result =
                await _productService.ActivateAsync(
                    id,
                    cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result =
                await _productService.DeactivateAsync(
                    id,
                    cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string searchTerm,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest(
                    "Search term is required.");

            return Ok(
                await _productService.SearchAsync(
                    searchTerm,
                    cancellationToken));
        }

        [HttpGet("category/{categoryId:guid}")]
        public async Task<IActionResult> GetByCategory(
            Guid categoryId,
            CancellationToken cancellationToken)
        {
            return Ok(
                await _productService.GetByCategoryAsync(
                    categoryId,
                    cancellationToken));
        }

        [HttpGet("brand/{brandId:guid}")]
        public async Task<IActionResult> GetByBrand(
            Guid brandId,
            CancellationToken cancellationToken)
        {
            return Ok(
                await _productService.GetByBrandAsync(
                    brandId,
                    cancellationToken));
        }

        [HttpGet("unit/{unitId:guid}")]
        public async Task<IActionResult> GetByUnit(
            Guid unitId,
            CancellationToken cancellationToken)
        {
            return Ok(
                await _productService.GetByUnitAsync(
                    unitId,
                    cancellationToken));
        }

        [HttpGet("sku/{sku}")]
        public async Task<IActionResult> GetBySku(
            string sku,
            CancellationToken cancellationToken)
        {
            var product =
                await _productService.GetBySkuAsync(
                    sku,
                    cancellationToken);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("sku-exists")]
        public async Task<IActionResult> CheckSkuExists(
            [FromQuery] string sku,
            [FromQuery] Guid? excludeId,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(sku))
                return BadRequest("SKU is required.");

            var exists =
                await _productService.CheckSkuExistsAsync(
                    sku,
                    excludeId,
                    cancellationToken);

            return Ok(new
            {
                exists
            });
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveProducts(
            CancellationToken cancellationToken)
        {
            return Ok(
                await _productService.GetActiveProductsAsync(
                    cancellationToken));
        }

        [HttpGet("inactive")]
        public async Task<IActionResult> GetInactiveProducts(
            CancellationToken cancellationToken)
        {
            return Ok(
                await _productService.GetInactiveProductsAsync(
                    cancellationToken));
        }
    }

}
