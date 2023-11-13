using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.SubjectsDTO;
using Microsoft.AspNetCore.Mvc;

namespace FM_MyStat.Web.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        #region Get All page
        public async Task<IActionResult> GetAll()
        {
            return View();
        }
        #endregion

        #region Create page
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSubjectDTO model)
        {
            return View(model);
        }
        #endregion

        #region Update page
        public async Task<IActionResult> Update(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditSubjectDTO model)
        {
            return View();
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            return View("Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteById(int Id)
        {
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

    }
}
