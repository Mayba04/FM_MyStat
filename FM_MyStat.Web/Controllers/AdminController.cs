using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
