using Database.Entities;


namespace Services.Services.Interfaces
{
    public interface ICartDetailService
    {
        // mutation
        public CartDetail CreateCartDetail(int ShoppingCartId, int ProductId, int quantity); // crea un detalle de carrito

        public void  DeleteCartDetail(int id); // elimina un detalle de carrito por id

    }
}