using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using FM_MyStat.Core.Entities;
using System.Security.Claims;
using FM_MyStat.Core.DTOs.SubjectsDTO;

namespace FM_MyStat.Web.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IGroupService _groupService;
        private readonly ISubjectService _subjectService;
        public LessonController(ILessonService lessonService, IGroupService groupService, ISubjectService subjectService)
        {
            this._lessonService = lessonService;
            _groupService = groupService;
            _subjectService = subjectService;
        }
        public IActionResult Index()
        {
            return View();
        }

        private async void LoadGroups()
        {
            var userId = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
            ServiceResponse<List<GroupDTO>, object> result = await _groupService.GetGroupDTOByTeacher(userId);
            @ViewBag.GroupList = new SelectList((System.Collections.IEnumerable)result,
                nameof(GroupDTO.Id), nameof(GroupDTO.Name)
              );
        }

        private async void LoadSubjects()
        {
            var userId = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
            ServiceResponse<List<SubjectDTO>, object> result = await _subjectService.GetSubjectDTOByTeacher(userId);
            @ViewBag.SubjectList = new SelectList((System.Collections.IEnumerable)result,
                nameof(Subject.Id), nameof(Subject.Name)
              );
        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _lessonService.GetAll();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
