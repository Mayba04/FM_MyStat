using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.SubjectsDTO;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Validation.Subject;

namespace FM_MyStat.Web.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public IActionResult Index1()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var subject = await _subjectService.GetAll();
            return View(subject);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSubjectDTO model)
        {
            var validator = new CreateSubjectValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var subjectTask = _subjectService.GetAll();
                List<SubjectDTO> subjects = await subjectTask;
                bool containsSubject = subjects.Any(cat => cat.Name == model.Name);
                if (containsSubject)
                {
                    ViewBag.AuthError = "Such a subjects already exists";
                    return View();
                }
                await _subjectService.Create(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subjectDto = await _subjectService.Get(id);

            if (subjectDto == null)
            {
                ViewBag.AuthError = "Subject not found.";
                return RedirectToAction(nameof(GetAll));
            }

            return View(subjectDto);
        }

        
        public async Task<IActionResult> DeleteSubject(int Id)
        {
            await _subjectService.Delete(Id);
            return RedirectToAction(nameof(GetAll));
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _subjectService.Get(Id);
            if (result != null)
            {
                return View(result);
            }
            ViewBag.AuthError = "An error occurred";
            return RedirectToAction(nameof(GetAll));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditSubjectDTO model)
        {
            var result = await _subjectService.GetByName(model.Name);
            if (result!=null)
            {
                ViewBag.AuthError = "Subjects exists.";
                return View(model);
            }
            var validator = new EditSubjectValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _subjectService.Update(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);
        }
    }
}
