using _2DGameMonogameApi.Classes;

namespace _2DGameMonogameApi.Authentification.Interface
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
       
        void CreateUser(Users user);
        
    }
}
