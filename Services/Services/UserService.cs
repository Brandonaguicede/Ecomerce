using Database.EcommerceDbContext;
using Database.Entities;
using System.Security.Cryptography.Xml;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class UserService : IUserService
    {

        private readonly MyEcommerceDbContext _dbContext;

        public UserService(MyEcommerceDbContext dbcontext)
        {
            _dbContext = dbcontext;    
        }
        public List<User> UserList()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public User RegisterUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Crea automáticamente el carrito
            var cart = new ShoppingCart
            {
                UserId = user.Id,
                Total = 0
            };

            _dbContext.ShoppingCarts.Add(cart);
            _dbContext.SaveChanges();

            return user;
        }


    }

}