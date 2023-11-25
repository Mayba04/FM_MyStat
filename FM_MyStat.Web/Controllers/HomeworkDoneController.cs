using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class HomeworkDoneController : Controller
    {
        public HomeworkDoneController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
