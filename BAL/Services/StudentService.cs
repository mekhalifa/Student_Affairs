
using BAL.Interfaces;
using DAL.Entities;
using DAL.Enums;
using DAL.Helpers;
using DAL.Interfaces;
using DAL.Repos;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      

        public ( List<StudentListVM> StudentList, int Total) GetData(JqueryDatatableParam param)
        {
            try
            {
                var list = _unitOfWork.Students.GetAllWithClasses();

                if (!string.IsNullOrEmpty(param.classId) && int.TryParse(param.classId, out int classId) && classId != 0)
                {
                    list = list.Where(x => x.ClassID == classId);
                }

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    list = list.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower()));
                }

                var sortColumnIndex = Convert.ToInt32(param.iSortCol_0);
                var sortDirection = param.sSortDir_0;

                if (sortColumnIndex == 1)
                {
                    list = sortDirection == "asc" ? list.OrderBy(c => c.Gender) : list.OrderByDescending(c => c.Gender);
                }
                else if (sortColumnIndex == 2)
                {
                    list = sortDirection == "asc" ? list.OrderBy(c => c.Class.Name) : list.OrderByDescending(c => c.Class.Name);
                }
                else
                {
                    list = sortDirection == "asc" ? list.OrderBy(c => c.Name) : list.OrderByDescending(c => c.Name);
                }
                var listResult =  list.Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength).ToList();

                var displayResult = listResult.Select(m => new StudentListVM()
                {
                    ID = m.ID,
                    Name = m.Name,
                    Gender = GetGenderEnum(m.Gender).ToString(),
                    ClassName = m.Class != null ? m.Class.Name : "Not Assigned Yet"

                }).ToList();
                var totalRecords =  list.Count();

                return (displayResult,totalRecords);

            }
            catch (Exception ex)
            {
                ErrHandler.WriteLog(ex.Message, "GETSTUDENTSDATA");
            }
            return (new List<StudentListVM>(), 0);
        }

        public StudentVM GetStudent(int id = 0)
        {
            StudentVM studentVM = new StudentVM();

            if (id == 0)
                return studentVM;
            else
            {
                try
                {
                    var findStudent = _unitOfWork.Students.GetById(id);
                    studentVM = new StudentVM()
                    {
                        ID = findStudent.ID,
                        Name = findStudent.Name,
                        Gender = GetGenderEnum(findStudent.Gender),
                        Address = findStudent.Address,
                        BirthDate = findStudent.BirthDate,
                        ClassID = findStudent.ClassID,
                        EmailAddress = findStudent.EmailAddress,
                        PhoneNumber = findStudent.PhoneNumber,
                        Photo = findStudent.Photo,
                    };

                }
                catch (Exception ex)
                {
                    ErrHandler.WriteLog(ex.Message, "GETSTUDENTDATA");
                }

                return studentVM;
            }
        }

        public Response AddOrEdit(StudentVM studentVM)
        {
            Response response = new Response();
            try
            {
                if (studentVM.ID == 0)
                {
                    Student newStudent = new Student()
                    {
                        Name = studentVM.Name,
                        Gender = (int)studentVM.Gender,
                        Address = studentVM.Address,
                        BirthDate = studentVM.BirthDate,
                        ClassID = studentVM.ClassID,
                        EmailAddress = studentVM.EmailAddress,
                        PhoneNumber = studentVM.PhoneNumber,
                        Photo = studentVM.Photo
                    };
                    _unitOfWork.Students.Add(newStudent);
                    _unitOfWork.Complete();
                    response.Status = true;
                    response.Message = "Added Successfully";

                }
                else
                {
                    Student findStudent = _unitOfWork.Students.GetById(studentVM.ID);
                    if (findStudent != null)
                    {
                        findStudent.Name = studentVM.Name;
                        findStudent.Gender = (int)studentVM.Gender;
                        findStudent.Address = studentVM.Address;
                        findStudent.BirthDate = studentVM.BirthDate;
                        findStudent.ClassID = studentVM.ClassID;
                        findStudent.EmailAddress = studentVM.EmailAddress;
                        findStudent.PhoneNumber = studentVM.PhoneNumber;
                        findStudent.Photo = studentVM.Photo;

                        _unitOfWork.Students.Edit(findStudent);
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
                ErrHandler.WriteLog(ex.Message, "AddOrEditStudentData");
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
                Student findStudent = _unitOfWork.Students.GetById(id);
                if (findStudent != null)
                {
                    _unitOfWork.Students.Remove(findStudent);

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
                ErrHandler.WriteLog(ex.Message, "DELETESTUDENTDATA");
                response.Status = false;
                response.Message = "Deleted Failed";
            }
            return response;

        }
        private Gender GetGenderEnum(int gender)
        {


            if (gender == 0)
                return Gender.None;

            else if (gender == 1)
                return Gender.Male;

            else if (gender == 2)
                return Gender.Female;
            else
                return Gender.None;
        }

      
    }
}
