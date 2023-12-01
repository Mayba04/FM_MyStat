using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.HomeworkServices;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.Homework;
using FM_MyStat.Web.Models.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public async Task<IActionResult> Homeinspection(int Id)
        {
            var homeworks = await _homeworkDoneService.GetAll(Id);

            return View(homeworks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetMark(HomeworkDoneDTO model)
        {
            var validator = new SetMarkValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _homeworkDoneService.Update(model);
                return RedirectToAction("GetAll", "Homework");
            }
            return RedirectToAction(nameof(Homeinspection));
        }


        public async Task<IActionResult> Download(int id)
        {
            var (fileContents, contentType, fileName) = await _homeworkDoneService.DownloadHomeworkFileAsync(id);

            if (fileContents == null || contentType == null || fileName == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return File(fileContents, contentType, fileName);
        }
    }
}
