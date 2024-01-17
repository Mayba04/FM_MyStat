using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.Group;
using FM_MyStat.Core.Validation.Subject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FM_MyStat.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly TeacherService _teacherService;

        public GroupController(IGroupService groupService, TeacherService teacherService)
        {
            _groupService= groupService;
            _teacherService = teacherService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var group = await _groupService.GetAll();
            return View(group);
        }

        private async Task LoadTeachers()
        {
            var serviceResponse = await _teacherService.GetAllAsync();
            if (serviceResponse != null && serviceResponse.Success)
            {
                List<TeacherDTO> result = serviceResponse.Payload;
                var teacherSelectList = result.Select(t => new SelectListItem
                {
                    Value = t.TeacherId.ToString(), 
                    Text = $"{t.FirstName} {t.SurName} {t.LastName}"
                });

                ViewBag.TeacherList = new SelectList(teacherSelectList, "Value", "Text");
            }
            else
            {
                ViewBag.TeacherList = Enumerable.Empty<SelectListItem>();
            }
        }

        public async Task<IActionResult> CreateAsync()
        {
            await LoadTeachers();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGroupDTO model)
        {
            var validator = new CreateGroupValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var groupTask = _groupService.GetAll();
                List<GroupDTO> groups = await groupTask;
                bool containsGroup = groups.Any(cat => cat.Name == model.Name);
                if (containsGroup)
                {
                    ViewBag.AuthError = "Such a group already exists";
                    return View();
                }
                await _groupService.Create(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _groupService.GetEditGroupDTO(id);
            if (result.Success)
            {
                await LoadTeachers();
                return View(result.Payload);
            }
            ViewBag.AuthError = result.Errors.FirstOrDefault();
            return RedirectToAction(nameof(GetAll));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditGroupDTO model)
        {
            var validator = new EditGroupValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _groupService.Update(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var groupDto = await _groupService.Get(id);

            if (groupDto == null)
            {
                ViewBag.AuthError = "Group not found.";
                return RedirectToAction(nameof(GetAll));
            }

            return View(groupDto);
        }
        public async Task<IActionResult> DeleteGroup(int Id)
        {
            await _groupService.Delete(Id);
            return RedirectToAction(nameof(GetAll));
        }

    }
}
