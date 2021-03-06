#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Database;
using Restaurant.Models;
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
                    return NotFound();
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
