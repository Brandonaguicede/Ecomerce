using Database.Entities;
using Services;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace BackendEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

       
        // GET api/orders/user/10/list
        [HttpGet("user/{userId}/list")]
        public List<Order> GetlistByUser(int userId)
        {
            return _orderService.OrderListByUser(userId);
        }

        // POST: api/Order/checkout?userId=5
        [HttpPost("checkout")]
        public IActionResult Checkout(int userId)
        {
            try
            {
                var order = _orderService.CreateOrderFromCart(userId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: api/orders/send-email/5
        [HttpPost("send-email/{orderId}")]
        public IActionResult SendOrderEmail(int orderId)
        {
            try
            {
                var order = _orderService.GetOrderById(orderId);
                if (order == null)
                    return NotFound(new { error = "Orden no encontrada" });

                _orderService.SendEmail(order);
                return Ok(new { message = "Correo de confirmación enviado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


    }
}

