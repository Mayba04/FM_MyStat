using FM_MyStat.Core.Services.Users;
using FM_MyStat.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FM_MyStat.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AdministratorService _adminService;
        private readonly StudentService _studentService;
        private readonly TeacherService _teacherService;
        public HomeController(AdministratorService adminService, StudentService studentService, TeacherService teacherService)
        {
            _adminService = adminService;
            _studentService = studentService;
            _teacherService = teacherService;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}