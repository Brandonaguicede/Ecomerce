using Database.EcommerceDbContext;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;
using Services.Services.Interfaces;


namespace Services.Services
{

    public class OrderService : IOrderService
    {

        private readonly MyEcommerceDbContext _dbContext;

        public OrderService(MyEcommerceDbContext dbcontext)
        {
            _dbContext = dbcontext;

        }


        public Order CreateOrderFromCart(int userId)  // Crea una orden a partir del carrito de compras del usuario
        {
            // Obtener el carrito actual
            var currentCart = _dbContext.ShoppingCarts
                .FirstOrDefault(c => c.UserId == userId && c.IsActive);

            if (currentCart == null)
                throw new Exception("Carrito no encontrado para el usuario.");

            if (currentCart.Total <= 0)
                throw new Exception("El carrito está vacío.");

            // Crear la orden con el carrito actual
            var order = new Order
            {
                UserId = userId,
                ShoppingCartId = currentCart.Id,
                TotalAmount = currentCart.Total
            };
            _dbContext.Orders.Add(order);

            // Desactivar el carrito actual
            currentCart.IsActive = false;


            // Crear un nuevo carrito para el usuario
            var newCart = new ShoppingCart
            {
                UserId = userId,
                Total = 0,
                IsActive = true,
            };
            _dbContext.ShoppingCarts.Add(newCart);

            // Guardar todo
            _dbContext.SaveChanges();

            return order;
        } 
        
        public Order GetOrderById(int id)  // Obtiene una orden por su ID
        {
            return _dbContext.Orders.Find(id);
        }

        public Order GetOrderByUserId(int userId) // Obtiene una orden por ID de usuario
        {
            return _dbContext.Orders.FirstOrDefault(x => x.UserId == userId);    
        }

        public List<Order> OrderListByUser(int userId) // Obtiene una lista de órdenes por ID de usuario
        {
            List<Order> OrderListByUser = _dbContext.Orders.Where(x=> x.UserId == userId ).ToList();
            return OrderListByUser;
        }

        public Order SendEmail(Order order) // Envía un correo electrónico con los detalles de la orden
        {
            // Ruta de la plantilla HTML
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OrderConfirmation.html");
            string htmlBody = File.ReadAllText(templatePath);

            // Obtener datos del usuario y carrito
            var user = _dbContext.Users.Find(order.UserId);
            var cart = _dbContext.ShoppingCarts.Find(order.ShoppingCartId);
            var cartDetails = _dbContext.CartDetails
                .Where(cd => cd.ShoppingCartId == cart.Id)
                .ToList();

            // Construir las filas de productos
            var culture = new System.Globalization.CultureInfo("en-US");
            string articleRows = "";
            foreach (var detail in cartDetails)
            {
                           // Obtener el producto (desde relación o buscándolo)
                var product = detail.Product ?? _dbContext.Products.Find(detail.ProductId);
                if (product == null) continue;

                var productId = product.Id;
                var productName = product.ProductName;
                var unitPrice = product.Price;
                var quantity = detail.Quantity;
                
                articleRows += $"<tr><td>{productId}</td><td>{productName}</td><td>{quantity}</td><td>{unitPrice.ToString("C2", culture)}</td></tr>";

            }
            // Calcular montos para el subtotal y total             -- Nuevo aggregado para el total --
            
            double subtotal = cartDetails.Sum(cd => cd.SubTotal);
            double total = subtotal + (subtotal * 0.13);

            // Reemplazar los marcadores en el HTML
            htmlBody = htmlBody.Replace("{{Email}}", user?.Email ?? "");
            htmlBody = htmlBody.Replace("{{Name}}", user?.Name ?? "");
            htmlBody = htmlBody.Replace("{{PhoneNumber}}", user?.PhoneNumber ?? "");
            htmlBody = htmlBody.Replace("{{Address}}", user?.Address ?? "");
            htmlBody = htmlBody.Replace("{{ArticleRows}}", articleRows);
            htmlBody = htmlBody.Replace("{{SubtotalAmount}}", subtotal.ToString("C2", culture));
            htmlBody = htmlBody.Replace("{{TotalAmount}}", total.ToString("C2", culture));



            // Configuración del correo
            var mail = new MailMessage();
            mail.To.Add(user?.Email ?? "teofilo023g@gmail.com");
            mail.Subject = "Confirmación de Orden";
            mail.Body = htmlBody;
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("teofilo023g@gmail.com");

            // Configuración SMTP para Gmail
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("teofilo023g@gmail.com", "lwhmaumnycflytom");
                smtp.Send(mail);
            }

            return order;
        }
    }


}
  