#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Database;
using Restaurant.Models;
using Restaurant.ViewModels;

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
        public async Task<ActionResult<IEnumerable<GetOrderViewModel>>> GetOrders()
        {
            var orders = await _repository.GetAllOrdersAsync();
            return Ok(_mapper.Map<GetOrderViewModel[]>(orders));
        }

        // GET: api/order/5
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
        
        // POST: api/order
        // Gets only memo from the frontend. Make Order from current user's CartItems.
        [HttpPost]
        public async Task<ActionResult<GetOrderViewModel>> PostOrder(Restaurant.ViewModels.Manager.PostOrderViewModel postOrder)
        {
            try
            {
                //var user = await _repository.GetUserAsync(new Guid("51e68b27-494a-4927-a2f2-18cbbd5c8975")); // @todo should be changed to fetch current user after implementing auth0
                var user = await _repository.GetUserAsync(new Guid("9bac3a17-e096-4467-84ed-5790e26beb45")); // @todo should be changed to fetch current user after implementing auth0
                
                if (user.CartItems.Count < 1)
                {
                    return NotFound();
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
                user.CartItems.Clear();
  
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
    }
}
