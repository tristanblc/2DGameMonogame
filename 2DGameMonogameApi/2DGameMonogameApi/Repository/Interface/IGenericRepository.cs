using _2DGameMonogameApi.Classes;


namespace _2DGameMonogameApi.Repository.Repository.Interface
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        T? Get(Guid i);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        bool Delete(Guid id);

    }
}
