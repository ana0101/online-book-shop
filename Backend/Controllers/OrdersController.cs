using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;
using OnlineBookShop.Repositories;

namespace OnlineBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        [HttpGet("{applicationUserId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetOrders(string applicationUserId)
        {
            var orders = await _ordersRepository.GetOrdersAsync(applicationUserId);
            return Ok(orders);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PostOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _ordersRepository.PostOrderAsync(order);
            return Ok(order.Id);
        }

        [HttpPut("{id}/{newStatus}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutOrder(int id, string newStatus)
        {
            var order = await _ordersRepository.PutOrderAsync(id, newStatus);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var ok = await _ordersRepository.DeleteOrderAsync(id);
            if (ok == false)
                return NotFound();
            return NoContent();
        }
    }
}
