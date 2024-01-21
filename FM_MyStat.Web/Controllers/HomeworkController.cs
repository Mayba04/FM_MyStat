using FluentValidation;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.LessonServices;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.Group;
using FM_MyStat.Core.Validation.Homework;
using FM_MyStat.Core.Validation.Subject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace FM_MyStat.Web.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IGroupService _groupService;
        private readonly ILessonService _lessonService;
        private readonly TeacherService _teacherService;
        private readonly UserService _userService;
        public HomeworkController(
            IHomeworkService homeworkService,
            IGroupService groupService,
            ILessonService lessonService,
            TeacherService teacherService,
            UserService userService
            )
        {
            _homeworkService = homeworkService;
            _groupService = groupService;
            _lessonService = lessonService;
            _teacherService = teacherService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            ServiceResponse<UserDTO, object> response = await _userService.GetLoggedUser(HttpContext.User);
            List<HomeworkDTO> payload = await _homeworkService.GetByTeacherId(response.Payload.Id);
            return View(payload);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create(int? LessonId)
        {
            await LoadTeacher();
            @ViewBag.LessonId = LessonId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHomeworkDTO model)
        {
            var validator = new CreateHomeworkValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var groupTask = _homeworkService.GetAll();
                List<HomeworkDTO> groups = await groupTask;
                bool containsGroup = groups.Any(cat => cat.Title == model.Title);
                if (containsGroup)
                {
                    ViewBag.AuthError = "Such a homework already exists";
                    return View();
                }
                await _homeworkService.Create(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View();
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _homeworkService.GetCreateHomeworkDTO(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            ViewBag.AuthError = result.Errors.FirstOrDefault();
            return RedirectToAction(nameof(GetAll));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CreateHomeworkDTO model)
        {
            var validator = new CreateHomeworkValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _homeworkService.Update(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var homeworkDto = await _homeworkService.Get(id);

            if (homeworkDto == null)
            {
                ViewBag.AuthError = "Homework not found.";
                return RedirectToAction(nameof(GetAll));
            }

            return View(homeworkDto);
        }
        public async Task<IActionResult> DeleteHomework(int Id)
        {
            await _homeworkService.Delete(Id);
            return RedirectToAction(nameof(GetAll));
        }

        public async Task<IActionResult> Download(int id)
        {
            var (fileContents, contentType, fileName) = await _homeworkService.DownloadHomeworkFileAsync(id);

            if (fileContents == null || contentType == null || fileName == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return File(fileContents, contentType, fileName);
        }

        private async Task LoadTeacher()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ServiceResponse<TeacherDTO, object> result = await _teacherService.GetTeacherByAppUserIdAsync(userId);
            @ViewBag.IdTeacher = result.Payload.Id;
        }
    }
}
