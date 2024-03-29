﻿using FluentValidation.Results;
using FM_MyStat.Core.DTOs.NewsDTO;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using FM_MyStat.Core.Validation.User;
using FM_MyStat.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FM_MyStat.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly AdministratorService _administratorService;
        private readonly UserService _userService;
        private readonly INewsService _newsController;

        public AdminController(AdministratorService administratorService, UserService userService, INewsService newsController)
        {
            this._administratorService = administratorService;
            _userService = userService;
            _newsController = newsController;
        }
        public async Task<IActionResult> Index()
        {
            var payload = await _newsController.GetAllBySpec(5);
            return View(payload.Payload);
        }

        #region Log out page
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            ServiceResponse response = await _administratorService.SignOutAsync();
            if (response.Success == true)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(GetAll);
        }
        #endregion

        #region Get all users page
        public async Task<IActionResult> GetAll()
        {
            ServiceResponse<List<AdminDTO>, object> result = await _administratorService.GetAllAsync();
            return View(result.Payload);
        }
        #endregion


        #region Create admin page
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdminDTO model)
        {
            CreateUserValidation validaor = new CreateUserValidation();
            ValidationResult validationResult = await validaor.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse response = await _administratorService.CreateAdministratorAsync(model);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.CreateUserError = response.Errors.FirstOrDefault();
                return View();
            }
            ViewBag.CreateUserError = validationResult.Errors.FirstOrDefault();
            return View();
        }
        #endregion

        #region Delete user page
        public async Task<IActionResult> Delete(string id)
        {
            ServiceResponse<DeleteUserDTO, object> result = await _administratorService.GetDeleteUserDtoByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View(nameof(GetAll));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteUserDTO model)
        {
            ServiceResponse result = await _administratorService.DeleteAdministratorAsync(model);
            if (!result.Success)
            {
                ViewBag.GetAllError = result.Errors.FirstOrDefault();
            }
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        #region Edit other user page
        public async Task<IActionResult> Edit(string id)
        {

            ServiceResponse<EditUserDTO, object> result = await _administratorService.GetEditUserDtoByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserDTO model)
        {
            ValidationResult validationResult = await new EditUserValidation().ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _administratorService.EditAdministratorAsync(model);
                if (result.Success)
                {
                    return View(nameof(Index));
                }
                return View(nameof(Index));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(nameof(Edit));
        }
        #endregion


        public async Task<IActionResult> Profile(string Id)
        {
            var result = await _userService.GetEditUserDtoByIdAsync(Id);
            if (result.Success)
            {
                UpdateProfileVM profile = new UpdateProfileVM()
                {
                    UserInfo = (EditUserDTO)result.Payload
                };
                return View(profile);
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Profiles(EditUserPasswordDTO model)
        {

            var validator = new EditPasswordValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.ChangePasswordAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("Home","Index");
                }

                ViewBag.UpdatePasswordError = result.Errors.FirstOrDefault();
                return View(nameof(Profile));

            }
            ViewBag.UpdatePasswordError = validationResult.Errors.FirstOrDefault();
            return View(nameof(Profile));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ChangeProfile(EditUserDTO model)
        {
            var validator = new EditUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.ChangeMainInfoUserAsync(model);
                if (result.Success)
                {
                    if (result.Message.Contains("address"))
                    {
                        ServiceResponse response = await _administratorService.SignOutAsync();
                        if (response.Success == true)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    return View(nameof(Profile), new UpdateProfileVM() { UserInfo = model });
                }
                ViewBag.UserUpdateError = result.Payload;
                return View(nameof(Profile), new UpdateProfileVM() { UserInfo = model });
            }
            ViewBag.UserUpdateError = validationResult.Errors.FirstOrDefault();
            return View(nameof(Profile), new UpdateProfileVM() { UserInfo = model });
        }

    }
}
