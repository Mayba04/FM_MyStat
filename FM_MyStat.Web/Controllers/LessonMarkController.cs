using FM_MyStat.Core.DTOs.LessonsDTO.LessonMark;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
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
            return View();
        }

        public async Task<IActionResult> SubmitMark(int Id)
        {
            List<StudentMarkDTO> studentMarkDTO = await _lessonMarkService.GetAllStudentsByLesson(Id);
            return View(studentMarkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitMark(List<StudentMarkDTO> marks)
        {
            await _lessonMarkService.SetMarksStudents(marks);
            return RedirectToAction("GetAll","Lesson");
        }
    }
}
