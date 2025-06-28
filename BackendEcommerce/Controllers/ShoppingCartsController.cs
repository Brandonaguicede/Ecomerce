using Microsoft.AspNetCore.Mvc;
using Database.Entities;
using Services;
using Services.Services.Interfaces;

namespace BackendEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: api/ShoppingCart/user/5
        [HttpGet("user/{userId}")]
        public IActionResult GetCartByUserId(int userId)
        {
            var cart = _shoppingCartService.GetCartByUserId(userId);
            if (cart == null)
                return NotFound("Carrito no encontrado para el usuario.");

            return Ok(cart);
        }


        // GET: api/ShoppingCart/5
        [HttpGet("{cartId}")]
        public IActionResult GetCartById(int cartId)
        {
            var cart = _shoppingCartService.GetCartById(cartId);
            if (cart == null)
                return NotFound("Carrito no encontrado.");

            return Ok(cart);
        }

        // POST: api/ShoppingCart/add-product?userId=5&productId=2&quantity=3
        [HttpPost("add-product")]
        public IActionResult AddProductToCart(int userId, int productId, int quantity)
        {
            try
            {
                var detail = _shoppingCartService.AddProductToCart(userId, productId, quantity);
                return Ok(detail);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/ShoppingCart/remove-detail/10
        [HttpDelete("remove-detail/{cartDetailId}")]
        public IActionResult RemoveProductFromCart(int cartDetailId)
        {
            _shoppingCartService.RemoveProductFromCart(cartDetailId);
            return NoContent();
        }
    }
}
