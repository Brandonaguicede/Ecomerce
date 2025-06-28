using Database.Entities;
using Services;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;


namespace BackendEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService ;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public List<Category> Get()
        {
            return _categoryService.CategoryList();
        }


        // POST api/<CategoryController>/5

        [HttpPost]
        public Category Post([FromBody] Category category)
        {
            return _categoryService.CreateCategory(category);
        }

    }
}
