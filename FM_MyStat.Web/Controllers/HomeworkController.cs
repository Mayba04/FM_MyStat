using FluentValidation;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.LessonServices;
using FM_MyStat.Core.Validation.Group;
using FM_MyStat.Core.Validation.Homework;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly IHomeworkService _homeworkService;
        public HomeworkController(IHomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            return View(await _homeworkService.GetAll());
        }   

        public IActionResult Create()
        {
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditHomeworkDTO model)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View("Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteById(int Id)
        {
            return RedirectToAction(nameof(GetAll));
        }
    }
}
