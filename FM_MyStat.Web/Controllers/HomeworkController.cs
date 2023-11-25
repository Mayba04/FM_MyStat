using FluentValidation;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.LessonServices;
using FM_MyStat.Core.Validation.Group;
using FM_MyStat.Core.Validation.Homework;
using FM_MyStat.Core.Validation.Subject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FM_MyStat.Web.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IGroupService _groupService;
        private readonly ILessonService _lessonService;
        public HomeworkController(IHomeworkService homeworkService, IGroupService groupService, ILessonService lessonService)
        {
            _homeworkService = homeworkService;
            _groupService = groupService;
            _lessonService = lessonService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            return View(await _homeworkService.GetAll());
        }

        private async Task LoadGroups()
        {
            var userId = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
            ServiceResponse<List<GroupDTO>, object> result = await _groupService.GetGroupDTOByTeacher(userId);
            @ViewBag.GroupList = new SelectList((System.Collections.IEnumerable)result.Payload,
                nameof(GroupDTO.Id), nameof(GroupDTO.Name)
              );
        }

        private async Task LoadLessons()
        {
            var userId = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
            ServiceResponse<List<LessonDTO>, object> result = await _lessonService.GetLessonDTOByTeacher(userId);
            @ViewBag.LessonList = new SelectList((System.Collections.IEnumerable)result.Payload,
                nameof(LessonDTO.Id), nameof(LessonDTO.Name)
              );
        }

        public async Task<IActionResult> Create()
        {
            // LoadGroups();
            await LoadGroups();
            await LoadLessons();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeworkDTO model)
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
                var files = HttpContext.Request.Form.Files;
                model.File = files;
                await _homeworkService.Create(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _homeworkService.Get(id);
            if (result != null)
            {
                return View(result);
            }
            ViewBag.AuthError = "An error occurred";
            return RedirectToAction(nameof(GetAll));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditHomeworkDTO model)
        {
            var result = await _homeworkService.GetByName(model.Title);
            if (result != null)
            {
                ViewBag.AuthError = "Homework exists.";
                return View(model);
            }
            var validator = new EditHomeworkValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _homeworkService.Update(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHomework(HomeworkDTO model)
        {
            var validator = new CreateHomeworkValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                model.File = files;
                await _homeworkService.Create(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return RedirectToAction(nameof(Create));
        }
    }
}
