using DAL.Data;
using DAL.Interfaces;

namespace DAL.Repos
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly StudentAffairsDbContext _context;
        public UnitOfWork(StudentAffairsDbContext context)
        {
            _context = context;
            Students = new StudentRepository(_context);
            Classes = new ClassRepository(_context);
        }
        public IStudentRepository Students { get; private set; }
        public IClassRepository Classes { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
