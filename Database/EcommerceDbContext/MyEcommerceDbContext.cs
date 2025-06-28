using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics.Contracts;

//  se creo la base de datos en memoria //

namespace Database.EcommerceDbContext
           
{
    public class MyEcommerceDbContext : DbContext
    {
        protected override void OnConfiguring
          (DbContextOptionsBuilder optionBuilder)
        {

            //optionsBuilder.UseInMemoryDatabase(databaseName: "ApiDB");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 42));
            optionBuilder.UseMySql("server=localhost;user=root;password=14022004;database=Ecomerce", serverVersion);


        }

        public DbSet<Product> Products { get; set; } // Tabla de productos
        public DbSet<Category> Categories { get; set; } // Tabla de categorías
        public DbSet<User> Users { get; set; } // Tabla de usuarios
        public DbSet<CartDetail> CartDetails { get; set; } // Tabla de detalles del carrito
        public DbSet<Order> Orders { get; set; } // Tabla de pedidos
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } // Tabla de carritos de compra

        
    }
}
 