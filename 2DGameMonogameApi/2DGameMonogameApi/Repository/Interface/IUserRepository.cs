using _2DGameMonogameApi.Classes;

namespace _2DGameMonogameApi.Repository.Interface
{
    public interface IUserRepository
    {
        public Users GetByEmail(string email);
        public Users Get(Guid id);
    }
}
