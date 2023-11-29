using FluentValidation.Results;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.User;
using FM_MyStat.Web.Models.ViewModels;
using FM_MyStat.Web.Models.ViewModels.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FM_MyStat.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        private readonly IGroupService _groupService;

        public StudentController(StudentService studentService, IGroupService groupService)
        {
            this._studentService = studentService;
            this._groupService = groupService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
            ServiceResponse<DashboardStudentInfo, object> response = await _studentService.GetDashboardStudentInfo(userId);
            return View(response.Payload);
        }

        #region Log out page
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            ServiceResponse response = await _studentService.SignOutAsync();
            if (response.Success == true)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(GetAll);
        }
        #endregion

        #region Get all users page
        public async Task<IActionResult> GetAll()
        {
            ServiceResponse<List<StudentDTO>, object> result = await _studentService.GetAllAsync();
            return View(result.Payload);
        }
        #endregion

        #region Create admin page
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            await LoadGroups();
            return View();
        }

        private async Task LoadGroups()
        {
            List<GroupDTO> result = await _groupService.GetAll();
            @ViewBag.GroupList = new SelectList((System.Collections.IEnumerable)result,
                nameof(GroupDTO.Id), nameof(GroupDTO.Name)
              );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStudentDTO model)
        {
            CreateUserValidation validaor = new CreateUserValidation();
            ValidationResult validationResult = await validaor.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse response = await _studentService.CreateStudentAsync(model);
                if (response.Success)
                {
                    return RedirectToAction(nameof(GetAll));
                }
                ViewBag.CreateUserError = response.Errors.FirstOrDefault();
                return RedirectToAction();
            }
            ViewBag.CreateUserError = validationResult.Errors.FirstOrDefault();
            return RedirectToAction();
        }
        #endregion

        #region Delete user page
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            ServiceResponse<DeleteUserDTO, object> result = await _studentService.GetDeleteUserDtoByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return RedirectToAction(nameof(GetAll));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteUserDTO model)
        {
            ServiceResponse result = await _studentService.DeleteStudentAsync(model);
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
            ServiceResponse<EditStudentDTO, object> result = await _studentService.GetEditUserDtoByIdAsync(id);
            if (result.Success)
            {
                await LoadGroups();
                return View(result.Payload);
            }
            return RedirectToAction(nameof(GetAll));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(EditStudentDTO model)
        {
            ValidationResult validationResult = await new EditUserValidation().ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _studentService.EditStudentAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(GetAll));
                }
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            await LoadGroups();
            return RedirectToAction(nameof(Edit));
        }
        #endregion
        public async Task<IActionResult> AllHomeworks()
        {
            var userId = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
            List<HomeworkDTO> homeworks = new List<HomeworkDTO>();
            homeworks.Add(new HomeworkDTO
            {
                Id = 1,
                Title = "Math Assignment",
                Description = "Solve problems 1 to 10",
                GroupId = 101,
                Group = "Group A",
                LessonId = 201,
                Lesson = "Mathematics"
            });

            homeworks.Add(new HomeworkDTO
            {
                Id = 2,
                Title = "English Essay",
                Description = "Write an essay on a chosen topic",
                GroupId = 102,
                Group = "Group B",
                LessonId = 202,
                Lesson = "English Literature"
            });

            homeworks.Add(new HomeworkDTO
            {
                Id = 3,
                Title = "Science Experiment",
                Description = "Conduct a simple experiment and write a report",
                GroupId = 103,
                Group = "Group C",
                LessonId = 203,
                Lesson = "Physics"
            });
            homeworks.Add(new HomeworkDTO
            {
                Id = 4,
                Title = "History Research",
                Description = "Research and write about a historical event",
                GroupId = 104,
                Group = "Group D",
                LessonId = 204,
                Lesson = "World History"
            });
            homeworks.Add(new HomeworkDTO
            {
                Id = 5,
                Title = "Art Project",
                Description = "Create a piece of art using your preferred medium",
                GroupId = 105,
                Group = "Group E",
                LessonId = 205,
                Lesson = "Visual Arts"
            });

            homeworks.Add(new HomeworkDTO
            {
                Id = 6,
                Title = "Physical Education Challenge",
                Description = "Complete a fitness challenge and track your progress",
                GroupId = 106,
                Group = "Group F",
                LessonId = 206,
                Lesson = "Physical Education"
            });
            return View(homeworks);
        }

        public async Task<IActionResult> SubmitHomework()
        {
            return View();
        }
        #region Profile page
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Profile(string Id)
        {
            ServiceResponse<EditStudentDTO, object> result = await _studentService.GetEditUserDtoByIdAsync(Id);
            if (result.Success)
            {
                UpdateProfileStudentVM profile = new UpdateProfileStudentVM()
                {
                    StudentInfo = result.Payload,
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
                ServiceResponse result = await _studentService.ChangeMainInfoStudentAsync(model);
                if (result.Success)
                {
                    return View("Profile", new UpdateProfileStudentVM() { StudentInfo = model });
                }
                ViewBag.UserUpdateError = result.Errors.FirstOrDefault();
                return View("Profile", new UpdateProfileStudentVM() { StudentInfo = model });
            }
            ViewBag.UserUpdateError = validationResult.Errors.FirstOrDefault();
            return View("Profile", new UpdateProfileStudentVM() { StudentInfo = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePasswordInfo(EditUserPasswordDTO model)
        {
            EditPasswordValidation validator = new EditPasswordValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _studentService.ChangePasswordAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                ViewBag.UpdatePasswordError = result.Errors;
                return View(new UpdateProfileStudentVM() { StudentInfo = _studentService.GetEditUserDtoByIdAsync(model.Id).Result.Payload });
            }
            ViewBag.UpdatePasswordError = validationResult.Errors.FirstOrDefault();
            return View(new UpdateProfileStudentVM() { StudentInfo = _studentService.GetEditUserDtoByIdAsync(model.Id).Result.Payload });
        }
        #endregion
        #endregion
    }
}
