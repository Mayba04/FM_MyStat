using FM_MyStat.Core.DTOs.LessonsDTO.LessonMark;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services.LessonServices;
using FM_MyStat.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FM_MyStat.Web.Controllers
{
    public class LessonMarkController : Controller
    {
        private readonly ILessonMarkService _lessonMarkService;

        public LessonMarkController(ILessonMarkService lessonMarkService)
        {
            _lessonMarkService = lessonMarkService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new LessonMarksVM
            {
                students = await _lessonMarkService.GetAllStudents(),
                mark = new LessonMarkDTO(),
                lesson = new LessonDTO()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> SubmitMark(int Id)
        {
            LessonMarksVM model = new LessonMarksVM()
            {
                students = await _lessonMarkService.GetAllStudents(),
                mark = new LessonMarkDTO(),
                lesson = new LessonDTO()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitMark(LessonMarkDTO mark)
        {
            try
            {
                await _lessonMarkService.AddGrade(mark);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while submitting the mark.");
                return View("Index");
            }
        }
    }
}
