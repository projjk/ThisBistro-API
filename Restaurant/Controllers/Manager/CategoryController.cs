#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Database;
using Restaurant.Models;
using Restaurant.ViewModels;

namespace Restaurant.Controllers.Manager
{
    [Route("api/manager/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/manager/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _repository.GetAllCategoriesAsync());
        }

        // GET: api/manager/category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _repository.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/manager/category/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PostCategoryViewModel>> PutCategory(int id, PostCategoryViewModel postCategory)
        {
            try
            {
                if (id != postCategory.Id)
                {
                    return BadRequest("Id doesn't match");
                }

                var oldCategory = await _repository.GetCategoryAsync(id);
                if (oldCategory == null)
                {
                    return NoContent();
                }

                _mapper.Map(postCategory, oldCategory);

                if (await _repository.SaveChangesAsync())
                {
                    return postCategory;
                }
                return BadRequest("Failed to update database");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to put Category");
            }
        }

        // POST: api/manager/category
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(PostCategoryViewModel postCategory)
        {
            try
            {
                var category = _mapper.Map<Category>(postCategory);
                _repository.Add(category);
                
                if (await _repository.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
                }
                return BadRequest("Failed to save new Category");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save new Category");
            }
        }

        // DELETE: api/manager/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _repository.GetCategoryAsync(id);
                if (category == null)
                {
                    return NotFound("Failed to find the category to delete");
                }
                _repository.Remove(category);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to delete the category");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the category");
            }
        }

    }
}
