using DAL.Entities;
using System.Linq;

namespace DAL.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        IQueryable<Student> GetAllWithClasses();
    }
}
