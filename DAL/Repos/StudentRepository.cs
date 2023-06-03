using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repos
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentAffairsDbContext context) : base(context)
        {
        }

        public IQueryable<Student> GetAllWithClasses()
        {
            return _context.Students.Include(m=>m.Class).AsQueryable();

        }
    }
}
