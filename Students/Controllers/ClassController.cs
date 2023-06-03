using System.Web.Mvc;
using System.Linq;
using BAL.Interfaces;
using DAL.Helpers;
using DAL.ViewModels;

namespace Students.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData(JqueryDatatableParam param)
        {
            var result = _classService.GetData(param);

            return Json(new
            {
                param.sEcho,
                iTotalRecords = result.Total,
                iTotalDisplayRecords = result.Total,
                aaData = result.ClassList
            }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
           return View(_classService.GetClass(id));
        }

        [HttpPost]
        public  ActionResult AddOrEdit(ClassVM classVM)
        {
           var result = _classService.AddOrEdit(classVM);
            return Json(new { success = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _classService.Delete(id);
            return Json(new { success = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetClassSelect2List(string q)
        {
            var result = _classService.GetClassSelect2ListVM(q);
            return Json(new { items = result.Append(new ClassSelect2ListVM() { id = "0", text = "All" }) }, JsonRequestBehavior.AllowGet);
        }

    }

}