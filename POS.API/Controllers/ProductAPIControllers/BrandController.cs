using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Product.Application.Dtos.Brand;
using POS.Product.Application.Interfaces.BrandInterfaces;

namespace POS.API.Controllers.ProductAPIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandController(IBrandService service)
        {
            _service = service;
        }

        // GET: /api/brands
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(
                cancellationToken);

            return Ok(result);
        }

        // GET: /api/brands/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(
                id,
                cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: /api/brands/{id}/products
        [HttpGet("{id:guid}/products")]
        public async Task<IActionResult> GetProducts(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetProductsAsync(
                id,
                cancellationToken);

            return Ok(result);
        }

        // POST: /api/brands
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateBrandRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        // PUT: /api/brands/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateBrandRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(
                id,
                request,
                cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE: /api/brands/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _service.DeleteAsync(
                id,
                cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // PATCH: /api/brands/{id}/activate
        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> Activate(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _service.ActivateAsync(
                id,
                cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // PATCH: /api/brands/{id}/deactivate
        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _service.DeactivateAsync(
                id,
                cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // GET: /api/brands/search?term=coke
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string term,
            CancellationToken cancellationToken)
        {
            var result = await _service.SearchAsync(
                term,
                cancellationToken);

            return Ok(result);
        }

    }
}
