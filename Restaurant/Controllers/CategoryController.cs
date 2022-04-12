#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Database;
using Restaurant.Models;
using Restaurant.ViewModels;
using Restaurant.ViewModels.Manager;

namespace Restaurant.Controllers
{
    [Route("api/category")]
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

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _repository.GetAllCategoriesAsync());
        }

        // GET: api/category/5
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
    }
}
