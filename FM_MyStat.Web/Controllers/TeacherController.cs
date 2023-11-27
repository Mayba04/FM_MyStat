using FluentValidation.Results;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.User;
using FM_MyStat.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeacherService _teacherService;

        public TeacherController(TeacherService teacherService)
        {
            this._teacherService = teacherService;
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult Index()
        {
            return View();
        }

        #region Log out page
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            ServiceResponse response = await _teacherService.SignOutAsync();
            if (response.Success == true)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(GetAll);
        }
        #endregion

        #region Get all users page
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            ServiceResponse<List<TeacherDTO>, object> result = await _teacherService.GetAllAsync();
            return View(result.Payload);
        }
        #endregion

        #region Profile page
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Profile(string Id)
        {
            ServiceResponse<EditUserDTO, object> result = await _teacherService.GetEditUserDtoByIdAsync(Id);
            if (result.Success)
            {
                UpdateProfileTeacherVM profile = new UpdateProfileTeacherVM()
                {
                    TeacherInfo = result.Payload,
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
                ServiceResponse result = await _teacherService.ChangeMainInfoTeacherAsync(model);
                if (result.Success)
                {
                    return View("Profile", new UpdateProfileTeacherVM() { TeacherInfo = model });
                }
                ViewBag.UserUpdateError = result.Errors.FirstOrDefault();
                return View("Profile", new UpdateProfileTeacherVM() { TeacherInfo = model });
            }
            ViewBag.UserUpdateError = validationResult.Errors.FirstOrDefault();
            return View("Profile", new UpdateProfileTeacherVM() { TeacherInfo = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePasswordInfo(EditUserPasswordDTO model)
        {
            EditPasswordValidation validator = new EditPasswordValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _teacherService.ChangePasswordAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                ViewBag.UpdatePasswordError = result.Errors;
                return View(new UpdateProfileTeacherVM() { TeacherInfo = _teacherService.GetEditUserDtoByIdAsync(model.Id).Result.Payload });
            }
            ViewBag.UpdatePasswordError = validationResult.Errors.FirstOrDefault();
            return View(new UpdateProfileTeacherVM() { TeacherInfo = _teacherService.GetEditUserDtoByIdAsync(model.Id).Result.Payload });
        }
        #endregion

        #region Create admin page
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdminDTO model)
        {
            CreateUserValidation validaor = new CreateUserValidation();
            ValidationResult validationResult = await validaor.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse response = await _teacherService.CreateTeacherAsync(model);
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            ServiceResponse<DeleteUserDTO, object> result = await _teacherService.GetDeleteUserDtoByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View(nameof(GetAll));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteUserDTO model)
        {
            ServiceResponse result = await _teacherService.DeleteTeacherAsync(model);
            if (!result.Success)
            {
                ViewBag.GetAllError = result.Errors.FirstOrDefault();
            }
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        #region Edit other user page
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {

            ServiceResponse<EditUserDTO, object> result = await _teacherService.GetEditUserDtoByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserDTO model)
        {
            ValidationResult validationResult = await new EditUserValidation().ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _teacherService.EditTeacherAsync(model);
                if (result.Success)
                {
                    return View(nameof(Index));
                }
                return View(nameof(Index));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(nameof(Edit));
        }
        
        #endregion


    }
}
