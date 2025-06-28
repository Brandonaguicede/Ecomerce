using Database.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using  Database.EcommerceDbContext;
using Services.Services.Interfaces;


namespace Services.Services
{

    public class ProductService : IProductService
    {    

       private readonly MyEcommerceDbContext _dbcontext; // Contexto de la base de datos para acceder a las entidades


        public ProductService(MyEcommerceDbContext dbcontext)  // Constructor que recibe el contexto de la base de datos
        {
            _dbcontext = dbcontext;
        }

        public Product CreateProduct(Product product) // Método para crear un nuevo producto
        {
           
           _dbcontext.Products.Add(product);
           _dbcontext .SaveChanges();
            return  product;
        }

        public void DeleteProduct(int id)  // Método para eliminar un producto por su ID
        {
            Product producFound = _dbcontext.Products.Find(id);
             
            if (producFound == null)
            {

                 throw new Exception("Product not found");
            }

              _dbcontext.Products.Remove(producFound);
            _dbcontext.SaveChanges();


        }

        public List<Product> ProductList() // Método para obtener una lista de todos los productos
        {
             return  _dbcontext.Products.ToList();
        }

        public Product SearchProduct(int id) // Método para buscar un producto por su ID 
        {
            return _dbcontext.Products.Find(id);
        }

        

        public Product UpdateProduct(int id, Product product) // Método para actualizar un producto existente
        { 
            Product productFound = _dbcontext.Products.Find(id);

            if (productFound == null)
            {
                return null; 
            }

            productFound.ProductName = product.ProductName;
            productFound.Price = product.Price;
            productFound.Stock = product.Stock;
            productFound.CategoryId = product.CategoryId;


            _dbcontext.Products.Update(productFound);
            _dbcontext.SaveChanges();

            return productFound;
        }


    }

}


