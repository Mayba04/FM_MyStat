using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using FM_MyStat.Core.Entities;
using System.Security.Claims;
using FM_MyStat.Core.DTOs.SubjectsDTO;
using FM_MyStat.Core.Validation.Subject;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.Validation.Lesson;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using Microsoft.AspNetCore.Identity;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Users;

namespace FM_MyStat.Web.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IGroupService _groupService;
        private readonly ISubjectService _subjectService;
        private readonly TeacherService _teacherService;
        private readonly UserService _userService;
        public LessonController(ILessonService lessonService, IGroupService groupService, ISubjectService subjectService, TeacherService treerService, UserService userService)
        {
            this._lessonService = lessonService;
            _groupService = groupService;
            _subjectService = subjectService;
            _teacherService = treerService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        private async Task LoadGroups()
        {
            ServiceResponse<UserDTO, object> response = await _userService.GetLoggedUser(HttpContext.User);
            ServiceResponse<List<GroupDTO>, object> result = await _groupService.GetGroupDTOByTeacher(response.Payload.Id);
            @ViewBag.GroupList = new SelectList((System.Collections.IEnumerable)result.Payload,
                nameof(GroupDTO.Id), nameof(GroupDTO.Name)
              );
        }

        private async Task LoadSubjects()
        {
            ServiceResponse<UserDTO, object> response = await _userService.GetLoggedUser(HttpContext.User);
            ServiceResponse<List<SubjectDTO>, object> result = await _subjectService.GetSubjectDTOByTeacher(response.Payload.Id);
            @ViewBag.SubjectList = new SelectList((System.Collections.IEnumerable)result.Payload,
                nameof(Subject.Id), nameof(Subject.Name)
              );
        }

        private async Task LoadTeacher()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ServiceResponse<TeacherDTO, object> result = await _teacherService.GetTeacherByAppUserIdAsync(userId);
            @ViewBag.IdTeacher = result.Payload.Id;
        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _lessonService.GetAll();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            await LoadGroups();
            await LoadSubjects();
            await LoadTeacher();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLessonsDTO model)
        {
            var validator = new CreateLessonValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _lessonService.Create(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View();
        }

        public async Task<IActionResult> Edit(int Id)
        {
            await LoadGroups();
            await LoadSubjects();
            await LoadTeacher();
            var result = await _lessonService.Get(Id);
            if (result != null)
            {
                return View(result);
            }
            ViewBag.AuthError = "An error occurred";
            return RedirectToAction(nameof(GetAll));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditLessonsDTO model)
        {
            var result = await _lessonService.GetByName(model.Name);
            if (result != null)
            {
                ViewBag.AuthError = "Subjects exists.";
                return View(model);
            }
            var validator = new EditLessonValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _lessonService.Update(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var groupDto = await _lessonService.Get(id);

            if (groupDto == null)
            {
                ViewBag.AuthError = "Lesson not found.";
                return RedirectToAction(nameof(GetAll));
            }

            return View(groupDto);
        }
        public async Task<IActionResult> DeleteLesson(int Id)
        {
            await _lessonService.Delete(Id);
            return RedirectToAction(nameof(GetAll));
        }
    }
}
