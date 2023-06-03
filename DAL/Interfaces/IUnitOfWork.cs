using System;

namespace DAL.Interfaces
{

    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        IClassRepository Classes { get; }
        int Complete();
    }

}
