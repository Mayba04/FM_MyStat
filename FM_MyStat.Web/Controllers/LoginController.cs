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

namespace FM_MyStat.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;
        public LoginController(UserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Index()
        {
            return View(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            var validator = new UserLoginValidation();
            var validationResult = validator.Validate(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.LoginUserAsync(model);
                if (result.Success)
                {
                    var roleResult = await _userService.GetUserRoleAsync(model.Email);
                    if (roleResult.Payload != null)
                    {
                        if (roleResult.Payload == "Administrator")
                        {
                            return RedirectToAction("Index", "Admin", model);
                        }
                        else if (roleResult.Payload == "Student")
                        {
                            return RedirectToAction("Index", "Student", model);
                        }
                        else if (roleResult.Payload == "Teachr")
                        {
                            return RedirectToAction("Index", "Teachr", model);
                        }
                        else 
                        {
                            ViewBag.AuthError = result.Message;
                            return View(model);
                        }
                    }
                   
                }

                ViewBag.AuthError = result.Message;
                return View(model);
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);

        }
    }
}
