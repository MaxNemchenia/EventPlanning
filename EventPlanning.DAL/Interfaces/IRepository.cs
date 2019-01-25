namespace EventPlanning.DAL.Interfaces
{
    public interface IRepository<T> where T:class
    {
        void Create(T item);
    }
}
