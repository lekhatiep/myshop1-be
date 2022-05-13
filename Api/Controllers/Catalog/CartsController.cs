using Api.Dtos.CartItems;
using Api.Services.Carts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/<CartsController>
        [HttpGet("GetListCart")]
        public async Task<IActionResult> GetListCart()
        {
            var cart = await _cartService.GetCartUserById(1); //Admin
            var listCart = await _cartService.GetUserListCartItem(cart.Id);

            return Ok(listCart);
        }

        // GET api/<CartsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCartItemDto createCartItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            await _cartService.AddToCart(createCartItemDto);
            var cart = await _cartService.GetCartUserById(1); //Admin
            var listCart = await _cartService.GetUserListCartItem(cart.Id);

            return Ok(listCart);
        }

        // PUT api/<CartsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
