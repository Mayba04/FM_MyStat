using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.User;
using FM_MyStat.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FM_MyStat.Web.Controllers
{
    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(nameof(Index));
        }
    }
}