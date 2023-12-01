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

        public async Task<IActionResult> Create(int? LessonId)
        {
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

    }
}
