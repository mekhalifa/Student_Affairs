using System.Linq;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        void Add(T entity);
        void Edit(T entity);
        void Remove(T entity);
    }
}
