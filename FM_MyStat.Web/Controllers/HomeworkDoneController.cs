using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services.HomeworkServices;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class HomeworkDoneController : Controller
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IHomeworkDoneService _homeworkDoneService;
        private readonly IGroupService _groupService;
        private readonly ILessonService _lessonService;

        public HomeworkDoneController(IHomeworkService homeworkService, IHomeworkDoneService homeworkDoneService, IGroupService groupService, ILessonService lessonService)
        {
            _homeworkDoneService = homeworkDoneService;
            _homeworkService = homeworkService;
            _groupService = groupService;
            _lessonService = lessonService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
