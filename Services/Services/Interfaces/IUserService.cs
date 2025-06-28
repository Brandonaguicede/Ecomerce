using Database.Entities;

namespace Services.Services.Interfaces
{ 
public interface IUserService
{
    // mutations
    public User RegisterUser(User user); // Registra un nuevo usuario


    public  User GetUserById( int  id );  // Obtiene un usuario por su ID
        public List<User> UserList(); // Obtiene una lista de todos los usuarios

    }
}