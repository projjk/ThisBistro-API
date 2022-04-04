#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Database;
using Restaurant.ViewModels;

namespace Restaurant.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public MenuController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuViewModel>>> GetMenus()
        {
            var results = await _repository.GetAllMenusAsync();
            return _mapper.Map<MenuViewModel[]>(results);
        }

        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuViewModel>> GetMenu(int id)
        {
            var menu = await _repository.GetMenuAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return _mapper.Map<MenuViewModel>(menu);
        }
    }
}
