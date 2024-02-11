namespace _2DGameMonogameApi.Classes
{
    public class GamesSaves : BaseEntity
    {
        public List<Saves> _saves { get; private set; }
        private Users _user { get;  set; }
        public GamesSaves()
        {
            _saves = new List<Saves>();

        }

        public void setUser(Users user)
        {
            _user = user;
        }
    }
}
