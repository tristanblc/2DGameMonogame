
using _2DGameMonogameApi.Classes;
using Microsoft.EntityFrameworkCore;

namespace _2DGameMonogameApi
{
    public class ApplicationDbContext : DbContext
    {

        private string ConnectionString { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BDBus;Trusted_Connection=True;MultipleActiveResultSets=true";


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);


            optionsBuilder.UseSqlServer(ConnectionString);
        }


        public override DbSet<TEntity> Set<TEntity>()
        {

            return base.Set<TEntity>();
        }


        public DbSet<Users> _users { get; set; }
        public DbSet<Saves> _saves { get; set; }
        public DbSet<Tokens> _tokens { get; set; }
    }
}
