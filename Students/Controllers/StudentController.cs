using System.Web.Mvc;
using BAL.Interfaces;
using DAL.Helpers;
using DAL.ViewModels;

namespace Students.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;

        public StudentController(IStudentService studentService,IClassService classService)
        {
            _studentService = studentService;
            _classService = classService;
        }
       
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetData(JqueryDatatableParam param)
        {      
          var result =  _studentService.GetData(param);

            return Json(new
            {
                param.sEcho,
                iTotalRecords = result.Total,
                iTotalDisplayRecords = result.Total,
                aaData = result.StudentList
            }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            ViewBag.classList = new SelectList(_classService.GetClassList(), "ID", "Name", new { ID = "", Name = "None" });

            return View(_studentService.GetStudent(id));
        }


        [HttpPost]
        public ActionResult AddOrEdit(StudentVM studentVM)
        {
            if(ModelState.IsValid)
            {
                var result = _studentService.AddOrEdit(studentVM);
                return Json(new { success = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Model is not valid" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
           var result = _studentService.Delete(id);
            return Json(new { success = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }
    }

}