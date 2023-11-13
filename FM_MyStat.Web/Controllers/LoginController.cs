using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.User.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {

        private readonly AdministratorService _adminService;
        private readonly StudentService _studentService;
        private readonly TeacherService _teacherService;
        private readonly SignInManager<AppUser> _signInManager;
        public LoginController(AdministratorService adminService, StudentService studentService, TeacherService teacherService)
        {
            _adminService = adminService;
            _studentService = studentService;
            _teacherService = teacherService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous] //GET
        public IActionResult Login()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [AllowAnonymous] // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAdmin(LoginAdminDTO model)
        {
            return await LoginAction();
        }

        [AllowAnonymous] // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginStudent(LoginStudentDTO model)
        {
            return await LoginAction();
        }

        [AllowAnonymous] // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginTeacher(LoginTeacherDTO model)
        {
            return await LoginAction();
        }

        private async Task<IActionResult> LoginAction() 
        {
            return View();
            //    var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            //    var validator = new LoginAdminValidation();
            //    var validationResult = validator.Validate(model);

            //    if (validationResult.IsValid)
            //    {
            //        ServiceResponse result = null;

            //        if (model is LoginAdminDTO adminModel)
            //        {
            //            result = await _adminService.LoginAdminAsync(adminModel);
            //        }
            //        else if (model is DTOStudentLogin studentModel)
            //        {
            //            result = await _studentService.LoginStudentAsync(studentModel);
            //        }
            //        else if (model is DTOTeacherLogin teacherModel)
            //        {
            //            result = await _teacherService.LoginTeacherAsync(teacherModel);
            //        }

            //        if (result?.Success ?? false)
            //        {
            //            return RedirectToAction(nameof(Index));
            //        }

            //        ViewBag.AuthError = result?.Message ?? "An error occurred.";
            //        return View(model);
            //    }

            //    ViewBag.AuthError = validationResult.Errors[0];
            //    return View(model);
        }

    }
}
