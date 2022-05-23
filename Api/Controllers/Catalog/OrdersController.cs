using Api.Dtos.OrderItems;
using Api.Dtos.Orders;
using Api.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: api/<OrdersController>
        [Authorize(Contanst.NamePermissions.Orders.View)]
        [HttpGet("HistoryOrderByUser")]
        public async Task<IActionResult> HistoryOrderByUser(string status)
        {
            try
            {
                var userId = (int)_httpContextAccessor.HttpContext.Items["Id"];
                var listOrderItem = await _orderService.GetListHistoryOrderByUser(userId, status);//admin
                return Ok(listOrderItem?? new List<OrderItemDto>());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrdersController>
        [Authorize(Contanst.NamePermissions.Orders.Create)]
        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout([FromBody] CreateOrderDto create)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderService.ProcessCheckoutOrder(create.UserId);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
