using Database.Entities;


namespace Services.Services.Interfaces
{
    public interface ICategoryService
    {
        // mutation
         public Category CreateCategory(Category category); // crea una categoria
        public List<Category> CategoryList(); // lista de categorias


    }
}
