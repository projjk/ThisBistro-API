#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Database;
using Restaurant.Models;
using Restaurant.ViewModels;

namespace Restaurant.Controllers.Manager
{
    [Route("api/manager/menu")]
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

        // GET: api/manager/menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            return Ok(await _repository.GetAllMenusAsync());
        }

        // GET: api/manager/menu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _repository.GetMenuAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        // PUT: api/manager/menu/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Menu>> PutMenu(int id, PutMenuViewModel menu)
        {
            try
            {
                if (id != menu.Id)
                {
                    return BadRequest("Id doesn't match");
                }

                var oldMenu = await _repository.GetMenuAsync(id);
                if (oldMenu == null)
                {
                    return NoContent();
                }

                // remove from the previous category
                var oldCategory = await _repository.FindCategory(oldMenu);
                oldCategory?.Menus.Remove(oldMenu);

                if (menu.CategoryId > 0)
                {
                    var category = await _repository.GetCategoryAsync(menu.CategoryId);
                    if (category == null)
                    {
                        // Put it back to the original category
                        oldCategory?.Menus.Add(oldMenu);
                        return BadRequest("Category doesn't exist");
                    }
                    category.Menus.Add(oldMenu);
                }

                _mapper.Map(menu, oldMenu);
                if (await _repository.SaveChangesAsync())
                {
                    return oldMenu;
                }
                return BadRequest("Failed to update database");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to put Menu");
            }
        }

        // POST: api/manager/menu
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(PostMenuViewModel postMenu)
        {
            try
            {
                var menu = _mapper.Map<Menu>(postMenu);
                _repository.Add(menu);

                if (postMenu.CategoryId > 0)
                {
                    var category = await _repository.GetCategoryAsync(postMenu.CategoryId);
                    if (category == null)
                    {
                        _repository.Remove(menu);
                        return BadRequest("Category doesn't exist");
                    }
                    category.Menus.Add(menu);
                }

                if (await _repository.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetMenu), new { id = menu.Id }, menu);
                }
                return BadRequest("Failed to save new Menu");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save new Menu");
            }
        }

        // DELETE: api/manager/menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                var menu = await _repository.GetMenuAsync(id);
                if (menu == null)
                {
                    return NotFound("Failed to find the menu to delete");
                }
                _repository.Remove(menu);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                return BadRequest("Failed to delete the menu");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the menu");
            }
        }

    }
}
