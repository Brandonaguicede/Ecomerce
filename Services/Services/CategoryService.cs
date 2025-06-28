using Database.Entities;
using Database.EcommerceDbContext;
using Microsoft.EntityFrameworkCore;
using Services.Services.Interfaces;

namespace Services.Services  
{
    public class CategoryService : ICategoryService
    {
        private readonly MyEcommerceDbContext _dbContext;

        public CategoryService(MyEcommerceDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public List<Category> CategoryList()
        {
            return _dbContext.Categories.ToList();
        }

        public Category CreateCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return category;
        }

    }
}
