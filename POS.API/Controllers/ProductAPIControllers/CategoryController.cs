using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Product.Application.Dtos.Category;
using POS.Product.Application.Interfaces.CategoryInterfaces;

namespace POS.API.Controllers.ProductAPIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        // GET /api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var result =
                await _service.GetAllAsync(cancellationToken);

            return Ok(result);
        }

        // GET /api/categories/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.GetByIdAsync(
                    id,
                    cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET /api/categories/{id}/children
        [HttpGet("{id:guid}/children")]
        public async Task<IActionResult> GetChildren(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.GetChildrenAsync(
                    id,
                    cancellationToken);

            return Ok(result);
        }

        // GET /api/categories/{id}/parent
        [HttpGet("{id:guid}/parent")]
        public async Task<IActionResult> GetParent(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.GetParentAsync(
                    id,
                    cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET /api/categories/tree
        [HttpGet("tree")]
        public async Task<IActionResult> GetTree(
            CancellationToken cancellationToken)
        {
            var result =
                await _service.GetTreeAsync(
                    cancellationToken);

            return Ok(result);
        }

        // POST /api/categories
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.CreateAsync(
                    request,
                    cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        // PUT /api/categories/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.UpdateAsync(
                    id,
                    request,
                    cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE /api/categories/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.DeleteAsync(
                    id,
                    cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // PATCH /api/categories/{id}/activate
        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> Activate(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.ActivateAsync(
                    id,
                    cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // PATCH /api/categories/{id}/deactivate
        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.DeactivateAsync(
                    id,
                    cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // GET /api/categories/search?term=burger
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string term,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.SearchAsync(
                    term,
                    cancellationToken);

            return Ok(result);
        }

    }
}
