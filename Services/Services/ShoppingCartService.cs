using Database.EcommerceDbContext;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly MyEcommerceDbContext _dbContext;

        public ShoppingCartService(MyEcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Agrega un producto al carrito
        public CartDetail AddProductToCart(int userId, int productId, int quantity)
        {
            // Buscar solo el carrito ACTIVO del usuario
            var cart = _dbContext.ShoppingCarts
                .Include(c => c.CartDetails)
                .FirstOrDefault(c => c.UserId == userId && c.IsActive);

            // Si no existe, crear uno nuevo
            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId, Total = 0, IsActive = true, CartDetails = new List<CartDetail>() };
                _dbContext.ShoppingCarts.Add(cart);
                _dbContext.SaveChanges();
            }

            var product = _dbContext.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) throw new Exception("Producto no encontrado.");
            if (product.Stock < quantity) throw new Exception("Stock insuficiente.");

            double subtotal = product.Price * quantity;
            var detail = new CartDetail
            {
                ShoppingCartId = cart.Id,
                ProductId = productId,
                SubTotal = subtotal,
                Quantity = quantity
            };

            cart.CartDetails.Add(detail);
            cart.Total += subtotal; // Suma al total del carrito

            product.Stock -= quantity;

            _dbContext.SaveChanges();

            return detail;
        }

        // Obtiene un carrito completo con detalles
        public ShoppingCart GetCartById(int cartId)
        {
            return _dbContext.ShoppingCarts
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Product)
                .FirstOrDefault(c => c.Id == cartId);
        }

        // Obtiene el carrito por ID de usuario
        public ShoppingCart GetCartByUserId(int userId)
        {
            return _dbContext.ShoppingCarts
                .FirstOrDefault(c => c.UserId == userId && c.IsActive);
        }

        // Elimina un producto del carrito
        public void RemoveProductFromCart(int cartDetailId)
        {
            var detail = _dbContext.CartDetails.FirstOrDefault(d => d.Id == cartDetailId);
            if (detail == null) return;

            var cart = _dbContext.ShoppingCarts.FirstOrDefault(c => c.Id == detail.ShoppingCartId);

            _dbContext.CartDetails.Remove(detail);
            _dbContext.SaveChanges();

            if (cart != null)
            {
                cart.Total = _dbContext.CartDetails
                    .Where(cd => cd.ShoppingCartId == cart.Id)
                    .Sum(cd => cd.SubTotal);

                _dbContext.SaveChanges();
            }
        }
    }

}