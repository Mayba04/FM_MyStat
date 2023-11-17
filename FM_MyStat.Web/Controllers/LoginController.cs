using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace FM_MyStat.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;
        private Dictionary<string, string> RolesToControllers = new Dictionary<string, string>
        {
            {"Administrator", "Admin"},
            {"Student", "Student"},
            {"Teacher", "Teacher"}
        };
        private string? GetControllerForUser(string role) => RolesToControllers.GetValueOrDefault(role, null);
        public LoginController(UserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Index()
        {
            bool userAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (userAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(nameof(Login));
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            var validator = new UserLoginValidation();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                ViewBag.AuthError = validationResult.Errors[0];
                return View(model);
            }
            ServiceResponse result = await _userService.LoginUserAsync(model);
            if (!result.Success)
            {
                ViewBag.AuthError = result.Message;
                return View(model);
            }
            var roleResult = await _userService.GetUserRoleAsync(model.Email);
            if (roleResult.Payload != null)
            {
                string? controller = GetControllerForUser((string)roleResult.Payload);
                if (controller != null)
                {
                    return RedirectToAction("Index", controller);
                }
            }
            ViewBag.AuthError = result.Message;
            return View(model);
        }
    }
}
