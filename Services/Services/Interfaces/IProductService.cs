using Database.Entities;

namespace Services.Services.Interfaces
{

    public interface IProductService 
    {
        // Queries

        public List<Product> ProductList(); // Lista de productos
        public Product SearchProduct(int id); // Busca un producto por su ID

        //Mutations

        public Product CreateProduct(Product product); // Crea un nuevo producto
        public Product UpdateProduct(int id, Product product); // Actualiza un producto existente
        public void DeleteProduct(int id); // Elimina un producto por su ID

    }

}
