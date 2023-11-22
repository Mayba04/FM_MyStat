using FluentValidation.Results;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Index()
        {
            return View();
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
                return View();
            }
            ViewBag.CreateUserError = validationResult.Errors.FirstOrDefault();
            return View();
        }
        #endregion
    }
}
