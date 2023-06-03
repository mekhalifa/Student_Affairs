using DAL.Entities;
using DAL.Helpers;
using DAL.ViewModels;
using System.Collections.Generic;

namespace BAL.Interfaces
{
    public interface IClassService
    {
        IEnumerable<ClassVM> GetClassList(); 
        List<ClassSelect2ListVM> GetClassSelect2ListVM(string name);

        (List<ClassVM> ClassList, int Total) GetData(JqueryDatatableParam param);
        ClassVM GetClass(int id = 0);
        Response AddOrEdit(ClassVM classVM);
        Response Delete(int id);
    }
}
