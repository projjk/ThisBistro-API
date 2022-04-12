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
using Restaurant.ViewModels.Manager;

namespace Restaurant.Controllers
{
    [Route("api/order")]
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

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _repository.GetAllOrdersAsync());
        }

        // GET: api/order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _repository.GetOrderAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/order/5
        // Only used for changing status
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> PutOrder(int id, PutOrderViewModel order)
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
                
                _mapper.Map(order, oldOrder);
                if (await _repository.SaveChangesAsync())
                {
                    return oldOrder;
                }
                return BadRequest("Failed to update database");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to put Order");
            }
        }

        // DELETE: api/order/5
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
