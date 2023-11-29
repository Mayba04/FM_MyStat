using FM_MyStat.Core.DTOs.LessonsDTO.LessonMark;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.Services.LessonServices;
using FM_MyStat.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class LessonMarkController : Controller
    {
        private readonly LessonMarkService _lessonMarkService;

        public LessonMarkController(LessonMarkService lessonMarkService)
        {
            _lessonMarkService = lessonMarkService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new LessonMarksVM
            {
                students = await _lessonMarkService.GetAllStudents(),
                mark = new LessonMarkDTO(),  // Initialize an empty LessonMarkDTO
                lesson = new LessonDTO()  // Initialize an empty LessonDTO
            };

            return View(viewModel);
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
