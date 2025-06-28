using Database.Entities;

namespace Services.Services.Interfaces
{ 
public interface IOrderService
{
   // Queries //

   public Order GetOrderById(int id); // Obtiene una orden por su ID
   public List <Order> OrderListByUser(int userId); // Obtiene una lista de órdenes por ID de usuario

        // Mutations

   public Order CreateOrderFromCart(int userId); // Crea una orden a partir del carrito de compras del usuario
        public   Order SendEmail (Order order); // Envía un correo electrónico con los detalles de la orden

    }
}
