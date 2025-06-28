using Database.Entities;
using Database.EcommerceDbContext;
using Microsoft.EntityFrameworkCore;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class CartDetailService : ICartDetailService
    {

        private readonly MyEcommerceDbContext _dbContext;

        public CartDetailService(MyEcommerceDbContext dbcontext) // Constructor que recibe el contexto de la base de datos
        {
            _dbContext = dbcontext;

        }

        public CartDetail CreateCartDetail(int ShoppingCartId, int ProductId, int quantity) // Crea un detalle de carrito
        {
            // Obtener el carrito
            var cart = _dbContext.ShoppingCarts  // crea una variable cart que obtiene el carrito de compras
                .Include(c => c.CartDetails)
                .FirstOrDefault(c => c.Id == ShoppingCartId); // Busca el carrito por ID

            if (cart == null)
                throw new Exception("Carrito no encontrado");

            // Obtener el producto
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == ProductId); // Busca el producto por ID

            if (product == null)
                throw new Exception("Producto no encontrado");

            // Calcular subtotal
            double unitPrice = product.Price;
            double subtotal = unitPrice * quantity;

            // Crear nuevo detalle
            var detail = new CartDetail
            {
                ShoppingCartId = ShoppingCartId,
                ProductId = ProductId,
                SubTotal = subtotal,
                Quantity = quantity
            };

            cart.CartDetails.Add(detail);

            _dbContext.SaveChanges();

            return detail;
        }

        public void DeleteCartDetail(int id)
        {
            CartDetail cartDetail = _dbContext.CartDetails.Find(id);
            if (cartDetail != null)
            {
          
                throw new Exception("Detalle de carrito no encontrado");
            }
            _dbContext.CartDetails.Remove(cartDetail);
            _dbContext.SaveChanges();
        }
    }
}
