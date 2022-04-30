#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Database;
using Restaurant.Models;
using Restaurant.ViewModels;

namespace Restaurant.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public CartController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCartItemViewModel>>> GetCarts()
        {
            // @todo return only the user's own carts.
            return _mapper.Map<GetCartItemViewModel[]>(await _repository.GetAllCartsAsync());
        }

        // GET: api/cart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCartItemViewModel>> GetCart(int id)
        {
            var cart = await _repository.GetCartItemAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return _mapper.Map<GetCartItemViewModel>(cart);
        }

        // POST: api/cart
        [HttpPost]
        public async Task<ActionResult<GetCartItemViewModel>> PostCart(PostCartItemViewModel cart)
        {
            try
            {
                var menu = await _repository.GetMenuAsync(cart.MenuId);
                if (menu == null)
                {
                    return BadRequest("Menu doesn't exist");
                }

                var newCart = _mapper.Map<CartItem>(cart);
                newCart.Menu = menu;
                newCart.Quantity = 1;
                _repository.Add(newCart);
                //////////////@todo register this to the current user's carts collection
                var user = await _repository.GetUserAsync(new Guid("9bac3a17-e096-4467-84ed-5790e26beb45")); // @todo should get the current user
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unable to find the user");
                }
                user.CartItems.Add(newCart);

                if (await _repository.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetCart), new { id = newCart.Id }, _mapper.Map<GetCartItemViewModel>(newCart));
                }
                return BadRequest("Failed to save new Cart");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save new Cart");
            }
        }

        // PUT: api/cart/5
        // Only used for changing quantity
        [HttpPut("{id}")]
        public async Task<ActionResult<GetCartItemViewModel>> PutCart(int id, PutCartItemViewModel cart)
        {
            try
            {
                if (id != cart.Id)
                {
                    return BadRequest("Id doesn't match");
                }

                var oldCart = await _repository.GetCartItemAsync(id);
                if (oldCart == null)
                {
                    return NoContent();
                }

                if (cart.Quantity < 1)
                {
                    return BadRequest("Quantity error. Call Delete method to delete it.");
                }

                _mapper.Map(cart, oldCart);
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<GetCartItemViewModel>(oldCart);
                }
                return BadRequest("Failed to update database");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to put Cart");
            }
        }

        
        // DELETE: api/cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                var cart = await _repository.GetCartItemAsync(id);
                if (cart == null)
                {
                    return NotFound("Failed to find the cart to delete");
                }
                _repository.Remove(cart);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                return BadRequest("Failed to delete the cart");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the cart");
            }
        }
    }
}
