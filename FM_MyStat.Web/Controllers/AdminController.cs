using FM_MyStat.Core.DTOs.UsersDTO.User;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index(UserLoginDTO model)
        {
            return View();
        }
    }
}
