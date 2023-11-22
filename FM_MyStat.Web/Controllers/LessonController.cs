using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FM_MyStat.Core.Interfaces;

namespace FM_MyStat.Web.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService)
        {
            this._lessonService = lessonService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            return View(await _lessonService.GetAll());
        }
    }
}
