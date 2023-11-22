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

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.Success)
            {
                var User = await _userService.GetAppUserById(userId);
                var Token = await _userService.SetPasswordAsync(userId);
                return RedirectToAction("ResetPassword", new { email = User.Payload.Email, token = Token.Payload });
            }
            return Redirect(nameof(Index));
        }


        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _userService.ForgotPasswordAsync(email);
            if (result.Success)
            {
                ViewBag.AuthError = result.Message;
                return View(nameof(Login));
            }
            ViewBag.AuthError = result.Message;
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(PasswordRecoveryDTO model)
        {
            var validator = new ResetPasswordValidation();
            var validationresult = await validator.ValidateAsync(model);
            if (validationresult.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);
                if (result.Success)
                {
                    ViewBag.AuthError = result.Message;
                    return View(nameof(Login));
                }
                ViewBag.AuthError = result.Message;
            }
            ViewBag.AuthError = validationresult.Errors[0];
            return View();
        }
    }
}
