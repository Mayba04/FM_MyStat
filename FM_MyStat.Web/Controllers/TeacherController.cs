using FluentValidation.Results;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.SubjectsDTO;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
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
        private readonly UserService _userService;
        private readonly TeacherService _teacherService;
        private readonly INewsService _newsService;

        public TeacherController(TeacherService teacherService, UserService userService, INewsService newsService)
        {
            this._teacherService = teacherService;
            this._userService = userService;
            this._newsService = newsService;
        }
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Index()
        {
            var payload = await _newsService.GetAllBySpec(5);
            return View(payload.Payload);
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
                    if (result.Message.Contains("address"))
                    {
                        ServiceResponse response = await _teacherService.SignOutAsync();
                        if (response.Success == true)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
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
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.UpdatePasswordError = result.Errors;
                return View("Profile", new UpdateProfileTeacherVM() { TeacherInfo = _teacherService.GetEditUserDtoByIdAsync(model.Id).Result.Payload });
            }
            ViewBag.UpdatePasswordError = validationResult.Errors.FirstOrDefault();
            return View("Profile", new UpdateProfileTeacherVM() { TeacherInfo = _teacherService.GetEditUserDtoByIdAsync(model.Id).Result.Payload });
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
                    return RedirectToAction(nameof(GetAll));
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

        #region Update subjects
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditSubjects(int Id)
        {
            IEnumerable<SubjectUpdateDTO> subjects = await _teacherService.GetSubjectsForTeacher(Id);
            TeacherEditSubjectsViewModel viewModel = new TeacherEditSubjectsViewModel
            {
                TeacherId = Id,
                Subjects = subjects.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditSubjects(TeacherEditSubjectsViewModel viewModel)
        {
            await _teacherService.UpdateTeacherSubjects(viewModel.TeacherId, viewModel.Subjects);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        #region Info for teacher
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Students()
        {
            ServiceResponse<UserDTO, object> response = await _userService.GetLoggedUser(HttpContext.User);
            List<StudentDTO> students = await _teacherService.GetStudentsByTeacherId((int)response.Payload.TeacherId);
            return View(students);
        }
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Groups()
        {
            ServiceResponse<UserDTO, object> response = await _userService.GetLoggedUser(HttpContext.User);
            List<GroupDTO> groups = await _teacherService.GetGroupsByTeacherId((int)response.Payload.TeacherId);
            return View(groups);
        }
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Subjects()
        {
            ServiceResponse<UserDTO, object> response = await _userService.GetLoggedUser(HttpContext.User);
            List<SubjectDTO> subjects = await _teacherService.GetSubjectsByTeacherId((int)response.Payload.TeacherId);
            return View(subjects);
        }
        #endregion
    }
}
