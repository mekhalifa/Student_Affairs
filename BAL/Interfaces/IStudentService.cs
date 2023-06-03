
using DAL.Helpers;
using DAL.ViewModels;
using System.Collections.Generic;

namespace BAL.Interfaces
{
    public interface IStudentService
    {

        ( List<StudentListVM> StudentList, int Total) GetData(JqueryDatatableParam param);
        StudentVM GetStudent(int id = 0);
        Response AddOrEdit(StudentVM studentVM);
        Response Delete(int id);

    }
}
