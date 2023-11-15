using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdministratorService _administratorService;

        public AdminController(AdministratorService administratorService)
        {
            this._administratorService = administratorService;
        }
        public IActionResult Index(UserLoginDTO model)
        {
            return View();
        }

        #region Log out page
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            ServiceResponse response = await _administratorService.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Get all users page
        public async Task<IActionResult> GetAll()
        {
            ServiceResponse<List<AdminDTO>, object> result = await _administratorService.GetAllAsync();
            return View(result.Payload);
        }
        #endregion

        #region Profile page
        public async Task<IActionResult> Profile(string Id)
        {
            ServiceResponse<EditUserDTO, object> result = await _administratorService.GetEditUserDtoByIdAsync(Id);
            if (result.Success)
            {
                UpdateProfileAdminVM profile = new UpdateProfileAdminVM()
                {
                    AdminInfo = result.Payload,
                };
                return View(profile);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeMainInfo(EditUserDTO model)
        {
            EditUserValidation validator = new EditUserValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _administratorService.ChangeMainInfoAdministratorAsync(model);
                if (result.Success)
                {
                    return View("Profile", new UpdateProfileAdminVM() { AdminInfo = model });
                }
                ViewBag.UserUpdateError = result.Errors.FirstOrDefault();
                return View("Profile", new UpdateProfileAdminVM() { AdminInfo = model });
            }
            ViewBag.UserUpdateError = validationResult.Errors[0];
            return View("Profile", new UpdateProfileAdminVM() { AdminInfo = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePasswordInfo(EditUserPasswordDTO model)
        {
            UpdatePasswordValidation validator = new EditPasswordValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _administratorService.ChangePasswordAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                ViewBag.UpdatePasswordError = result.Errors;
                return View(new UpdateProfileAdminVM() { AdminInfo = _administratorService.GetEditUserDtoByIdAsync(model.Id).Result.Payload });
            }
            ViewBag.UpdatePasswordError = validationResult.Errors[0];
            return View(new UpdateProfileAdminVM() { AdminInfo = _administratorService.GetEditUserDtoByIdAsync(model.Id).Result.Payload });
        }
        #endregion

        #region Create admin page
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateAdminDTO model)
        {
            CreateUserValidation validaor = new CreateUserValidation();
            ValidationResult validationResult = await validaor.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse response = await _administratorService.CreateAdministratorAsync(model);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.CreateUserError = response.Errors.FirstOrDefault();
                return View();
            }
            ViewBag.CreateUserError = validationResult.Errors.FirstOrDefault();
            return View();
        }
        #endregion

        #region Delete user page
        public async Task<IActionResult> DeleteUser(string id)
        {
            ServiceResponse<DeleteAdminDTO, object> result = await _administratorService.GetDeleteUserDtoByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View(nameof(GetAll));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(DeleteAdminDTO model)
        {
            ServiceResponse result = await _administratorService.DeleteAdministratorAsync(model);
            if (!result.Success)
            {
                ViewBag.GetAllError = result.Errors.FirstOrDefault();
            }
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        
    }
}
