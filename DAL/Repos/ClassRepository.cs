using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repos
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        public ClassRepository(StudentAffairsDbContext context) : base(context)
        {
        }
    }
}
