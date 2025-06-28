using Database.Entities;

namespace Services.Services.Interfaces
{ 
public interface IShoppingCartService
{

    //Queries //

    public CartDetail AddProductToCart(int userId, int productId, int quantity); // agrega un producto al carrito de compras del usuario
        public ShoppingCart GetCartById(int cartId); // obtiene un carrito de compras por su ID
        public ShoppingCart GetCartByUserId(int userId); // obtiene un carrito de compras por ID de usuario

        //mutations
        public void RemoveProductFromCart(int cartDetailId); // elimina un producto del carrito de compras por ID de detalle de carrito



    }
}
