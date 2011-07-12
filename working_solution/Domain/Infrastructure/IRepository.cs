namespace Domain.Infrastructure
{
    public interface IRepository<T>
    {
        void save<T>(T entity);
        T get_by(long id);
    }
}