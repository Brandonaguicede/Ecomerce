using Database.Entities;
using Services;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.Services;


namespace BackendEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("Search/{Id}/")]
        public Product SearchProduct( int Id)
        {
             return _productService.SearchProduct(Id);

        }


        // GET: api/<ProductsController>
        [HttpGet ("list")]
        public List<Product> Get()
        {
            return _productService.ProductList();
        }

     

        // POST api/<ProductsController>
        [HttpPost]
        public Product Post([FromBody]Product product)
        {
            return _productService.CreateProduct(product);
        }


        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public Product Put(int id, [FromBody]Product product)
        {
            return _productService.UpdateProduct(id, product);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
    }
}
