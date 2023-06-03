
using BAL.Interfaces;
using DAL.Entities;
using DAL.Helpers;
using DAL.Interfaces;
using DAL.Repos;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Services
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public (List<ClassVM> ClassList, int Total) GetData(JqueryDatatableParam param)
        {
            try
            {
                var list = _unitOfWork.Classes.GetAll();

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    list = list.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower()));
                }

                var sortColumnIndex = Convert.ToInt32(param.iSortCol_0);
                var sortDirection = param.sSortDir_0;

                list = sortDirection == "asc" ? list.OrderBy(c => c.Name) : list.OrderByDescending(c => c.Name);

                var listResult =  list.Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength).ToList();

                var displayResult = listResult.Select(m => new ClassVM()
                {
                    ID = m.ID,
                    Name = m.Name,

                }).ToList();
                var totalRecords =  list.Count();

                return ( displayResult, totalRecords);

            }
            catch (Exception ex)
            {
                ErrHandler.WriteLog(ex.Message, "GETSTUDENTSDATA");
            }

            return (new List<ClassVM>(), 0);
        }

        public ClassVM GetClass(int id = 0)
        {
            ClassVM classVM = new ClassVM();

            if (id == 0)
                return classVM;
            else
            {
                try
                {
                    var findClass = _unitOfWork.Classes.GetById(id);
                    classVM = new ClassVM()
                    {
                        ID = findClass.ID,
                        Name = findClass.Name
                    };
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteLog(ex.Message, "GETSTUDENTDATA");
                }
                return classVM;
            }
        }

        public Response AddOrEdit(ClassVM classVM)
        {
            Response response = new Response();
            try
            {
                if (classVM.ID == 0)
                {
                    Class newClass = new Class()
                    {
                        Name = classVM.Name,
                    };
                    _unitOfWork.Classes.Add(newClass);
                    _unitOfWork.Complete();

                    response.Status = true;
                    response.Message = "Added Successfully";

                }
                else
                {
                    Class findClass = _unitOfWork.Classes.GetById(classVM.ID);
                    if (findClass != null)
                    {
                        findClass.Name = classVM.Name;

                        _unitOfWork.Classes.Edit(findClass);
                        _unitOfWork.Complete();

                        response.Status = true;
                        response.Message = "Updated Successfully";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not Found";
                    }
                }

            }
            catch (Exception ex)
            {
                ErrHandler.WriteLog(ex.Message, "AddOrEditClassData");
                response.Status = false;
                response.Message = "Failed";
            }
            return response;
        }

        public Response Delete(int id)
        {
            Response response = new Response();
            try
            {
                Class findClass = _unitOfWork.Classes.GetById(id);
                if (findClass != null)
                {
                    _unitOfWork.Classes.Remove(findClass);

                    _unitOfWork.Complete();

                    response.Status = true;
                    response.Message = "Deleted Successfully";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Not Found";
                }


            }
            catch (Exception ex)
            {
                ErrHandler.WriteLog(ex.Message, "DELETECLASSDATA");
                response.Status = false;
                response.Message = "Deleted Failed";
            }
            return response;
        }



        public List<ClassSelect2ListVM> GetClassSelect2ListVM(string name)
        {
            var list = new List<ClassSelect2ListVM>();

            if (!(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)))
            {
                list = _unitOfWork.Classes.GetAll().Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                        .Select(m => new ClassSelect2ListVM()
                        {
                            id = m.ID.ToString(),
                            text = m.Name,
                        })
                        .ToList();
            }
            return list;
        }



        IEnumerable<ClassVM> IClassService.GetClassList()
        {
            return _unitOfWork.Classes.GetAll().Select(m => new ClassVM()
            {
                ID = m.ID,
                Name = m.Name,
            }).AsEnumerable();
        }
    }
}
