using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;
using OnlineBookShop.Repositories;

namespace OnlineBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("_myAllowSpecificOrigins")]

    public class CartsController : ControllerBase
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IMapper _mapper;

        public CartsController(ICartsRepository cartsRepository, IMapper mapper)
        {
            _cartsRepository = cartsRepository;
            _mapper = mapper;
        }

        [HttpGet("user/{applicationUserId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetCarts(string applicationUserId)
        {
            var carts = await _cartsRepository.GetCartsAsync(applicationUserId);
            return Ok(carts);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _cartsRepository.GetCartAsync(id);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }

        [HttpPost]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> PostCart(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _cartsRepository.PostCartAsync(cart);
            return Ok(cart);
        }

        [HttpPut]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> PutCart(int id, int newQuantity)
        {
            var cart = await _cartsRepository.PutCartAsync(id, newQuantity);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var ok = await _cartsRepository.DeleteCartAsync(id);
            if (ok == false)
                return NotFound();
            return NoContent();
        }
    }
}
