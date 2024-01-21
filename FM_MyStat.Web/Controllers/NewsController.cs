using FluentValidation;
using FM_MyStat.Core.DTOs.NewsDTO;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services.LessonServices;
using FM_MyStat.Core.Validation.Lesson;
using FM_MyStat.Core.Validation.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Student, Teacher, Administrator")]
        public async Task<IActionResult> GetAll()
        {
            List<NewsDTO> payload = await _newsService.GetAll();
            return View(payload);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ManageNews()
        {
            List<NewsDTO> payload = await _newsService.GetAll();
            return View(payload);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewsDTO model)
        {
            var validator = new CreateNewsValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _newsService.Create(model);
                return RedirectToAction(nameof(ManageNews));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _newsService.GetEdit(Id);
            if (result != null)
            {
                return View(result);
            }
            ViewBag.AuthError = "An error occurred";
            return RedirectToAction(nameof(ManageNews));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditNewsDTO model)
        {
            var validator = new EditNewsValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _newsService.Update(model);
                return RedirectToAction(nameof(ManageNews));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int Id)
        {
            var newsDto = await _newsService.Get(Id);
            if (newsDto == null)
            {
                ViewBag.AuthError = "News not found.";
                return RedirectToAction(nameof(ManageNews));
            }
            return View(newsDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletenews(int Id)
        {
            await _newsService.Delete(Id);
            return RedirectToAction(nameof(ManageNews));
        }
    }
}
