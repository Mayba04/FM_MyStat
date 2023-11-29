using FM_MyStat.Core.DTOs.LessonsDTO.LessonMark;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class LessonMarkController : Controller
    {
        public LessonMarkController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetInfoLesson(string lessonId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitMark(LessonMarkDTO model)
        {
            return View();
        }
    }
}
