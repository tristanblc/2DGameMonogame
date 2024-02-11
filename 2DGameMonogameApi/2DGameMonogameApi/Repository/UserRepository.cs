
using _2DGameMonogameApi.Classes;
using AutoMapper;

namespace _2DGameMonogameApi.Repository
{
    public class UserRepository
    {
        public IMapper _mapper { get; }

        private ApplicationDbContext SqlContext { get; set; }

        public UserRepository(ApplicationDbContext apiContext)
        {
            SqlContext = apiContext;
        }

        public Users GetByEmail(string email)
        {
            return SqlContext.Set<Users>().Where(element => element.Email == email).FirstOrDefault();
        }


        public Users Get(Guid id)
        {
            return SqlContext.Find<Users>(id);
        }


    }

}
