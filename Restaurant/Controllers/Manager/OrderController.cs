#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Restaurant.Database;
using Restaurant.Models;
using Restaurant.ViewModels;
using Restaurant.ViewModels.Manager;

namespace Restaurant.Controllers.Manager
{
    [Route("api/manager/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public OrderController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/manager/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderViewModel>>> GetOrders()
        {
            var orders = await _repository.GetAllOrdersAsync();
            return Ok(_mapper.Map<GetOrderViewModel[]>(orders));
        }

        // GET: api/manager/order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderViewModel>> GetOrder(int id)
        {
            var order = await _repository.GetOrderAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return _mapper.Map<GetOrderViewModel>(order);
        }
        
        // POST: api/manager/order
        // Gets only memo from the frontend. Make Order from current user's CartItems.
        [HttpPost]
        public async Task<ActionResult<GetOrderViewModel>> PostOrder(PostOrderViewModel postOrder)
        {
            try
            {
                var user = await _repository.GetUserAsync(new Guid("0736c8d1-e45a-4ae8-8f93-b23bda993728")); // @todo should be changed to fetch current user after implementing auth0

                if (user.CartItems.Count < 1)
                {
                    return NoContent();
                }

                var order = new Order { Status = 0, Date = DateTime.UtcNow, User = user };
                _repository.Add(order);

                if (!string.IsNullOrWhiteSpace(postOrder.Memo))
                {
                    order.Memo = postOrder.Memo;
                }

                foreach (var cartItem in user.CartItems)
                {
                    var orderItem = _mapper.Map<OrderItem>(cartItem);
                    orderItem.Order = order;
                    _repository.Add(orderItem);

                    order.Price += orderItem.Menu.Price * orderItem.Quantity;
                }
  
                if (await _repository.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, _mapper.Map<GetOrderViewModel>(order));
                }
                return BadRequest("Failed to save new Order");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save new Order");
            }
        }
        
        // PUT: api/manager/order/5
        // Only used for changing status
        [HttpPut("{id}")]
        public async Task<ActionResult<GetOrderViewModel>> PutOrder(int id, PutOrderViewModel order)
        {
            try
            {
                if (id != order.Id)
                {
                    return BadRequest("Id doesn't match");
                }

                var oldOrder = await _repository.GetOrderAsync(id);
                if (oldOrder == null)
                {
                    return NoContent();
                }

                if (!Enum.IsDefined(typeof(Order.OrderStatusEnum), order.Status) )
                {
                    return BadRequest("status: out of range");
                }
                
                _mapper.Map(order, oldOrder);
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<GetOrderViewModel>(oldOrder);
                }
                return BadRequest("Failed to update database");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to put Order");
            }
        }

        // DELETE: api/manager/order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _repository.GetOrderAsync(id);
                if (order == null)
                {
                    return NotFound("Failed to find the order to delete");
                }
                _repository.Remove(order);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                return BadRequest("Failed to delete the order");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the order");
            }
        }
    }
}
