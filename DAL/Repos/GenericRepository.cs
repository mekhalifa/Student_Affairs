
using DAL.Interfaces;
using DAL.Data;
using System.Linq;

namespace DAL.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly StudentAffairsDbContext _context;
        public GenericRepository(StudentAffairsDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Edit(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
        }


        IQueryable<T> IGenericRepository<T>.GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
