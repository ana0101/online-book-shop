using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;
using OnlineBookShop.Repositories;
using System.Diagnostics;

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
            
         [HttpGet("{applicationUserId}/{bookId}")]
         [Authorize(Roles = "User")]
         public async Task<int> GetQuantity(string applicationUserId, int bookId)
         {
             var quantity = await _cartsRepository.GetQuantityAsync(applicationUserId, bookId);
             return quantity;
         }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PostCart(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _cartsRepository.PostCartAsync(cart);
            return Ok(cart);
        }

        [HttpPut("{applicationUserId}/{bookId}/{newQuantity}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PutCart(string applicationUserId, int bookId, int newQuantity)
        {
            var cart = await _cartsRepository.PutCartAsync(applicationUserId, bookId, newQuantity);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }

        [HttpDelete("{applicationUserId}/{bookId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteCart(string applicationUserId, int bookId)
        {
            var ok = await _cartsRepository.DeleteCartAsync(applicationUserId, bookId);
            if (ok == false)
                return NotFound();
            return NoContent();
        }
    }
}
