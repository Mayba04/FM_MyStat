using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services.Users
{
    public class AdministratorService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AdministratorService(
                UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager,
                RoleManager<IdentityRole> roleManager,
                EmailService emailService,
                IMapper mapper,
                IConfiguration configuration
            )
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._emailService = emailService;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        #region SignIn, SignOut
        public async Task<ServiceResponse> LoginAdminAsync(LoginAdminDTO model)
        {
            AppUser? user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse(false, "User or password incorect.");
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, true, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true);
                return new ServiceResponse(true, "User successfully loged in.");
            }
            if (result.IsNotAllowed)
            {
                return new ServiceResponse(false, "Confirm your email please");
            }
            if (result.IsLockedOut)
            {
                return new ServiceResponse(false, "Useris locked. Connect with your site admistrator.");
            }
            return new ServiceResponse(false, "User or password incorect");
        }
        public async Task<ServiceResponse> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return new ServiceResponse(true);
        }
        #endregion

        #region Create user, Delete user, Edit password user, Edit main info user
        public async Task<ServiceResponse> CreateAdminAsync(CreateAdminDTO model)
        {
            AppUser NewUser = _mapper.Map<CreateAdminDTO, AppUser>(model);
            IdentityResult result = await _userManager.CreateAsync(NewUser, model.Password);
            if (result.Succeeded)
            {
                await SendConfirmationEmailAsync(NewUser);
                return new ServiceResponse(true, "User has been added");
            }
            return new ServiceResponse(false, "Something went wrong", errors: result.Errors.Select(e => e.Description));
        }

        public async Task<ServiceResponse> DeleteAdminAsync(DeleteAdminDTO model)
        {
            AppUser userdelete = await _userManager.FindByIdAsync(model.Id);
            if (userdelete == null)
            {
                return new ServiceResponse(false, "User a was found");
            }
            IdentityResult result = await _userManager.DeleteAsync(userdelete);
            if (result.Succeeded)
            {
                return new ServiceResponse(true);
            }
            return new ServiceResponse(false, "something went wrong", errors: result.Errors.Select(e => e.Description));
        }

        public async Task<ServiceResponse> ChangePasswordAsync(EditAdminPasswordDTO model)
        {
            AppUser user = _userManager.FindByIdAsync(model.Id).Result;
            if (user == null) return new ServiceResponse(false, "User or password incorrect.", errors: new List<string>() { "User or password incorrect." });

            IdentityResult result = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).Result;
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return new ServiceResponse(true, "Password successfully updated");
            }
            return new ServiceResponse(false, "Error.", errors: result.Errors.ToList().Select(i => i.Description));
        }

        public async Task<ServiceResponse> ChangeMainInfoAdminAsync(EditAdminDTO newinfo)
        {
            AppUser user = await _userManager.FindByIdAsync(newinfo.Id);

            if (user != null)
            {
                // ...

                IdentityResult result = await _userManager.UpdateAsync(user);

                return (result.Succeeded) ?
                    new ServiceResponse(true, "Information has been changed") :
                    new ServiceResponse(false, "Something went wrong", errors: result.Errors.Select(e => e.Description));
            }
            return new ServiceResponse(false, "Not found user");
        }
        #endregion

        #region Confirm email and send token for confirm email
        public async Task SendConfirmationEmailAsync(AppUser user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            byte[] encodedToken = Encoding.UTF8.GetBytes(token);
            string validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["HostSettings:URL"]}/Dashboard/confirmemail?userid={user.Id}&token={validEmailToken}";

            string emailBody = $"<h1>Confirm your email</h1> <a href='{url}'>Confirm now!</a>";
            //await _emailService.SendEmailAsync(user.Email, "Email confirmation.", emailBody);
        }

        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string token)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse(false, "User not found");
            }

            byte[] decodedToken = WebEncoders.Base64UrlDecode(token);
            string narmalToken = Encoding.UTF8.GetString(decodedToken);

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, narmalToken);
            if (result.Succeeded)
            {
                return new ServiceResponse(true, "Email successfully confirmed.");
            }
            return new ServiceResponse(false, "Email not confirmed", errors: result.Errors.Select(e => e.Description));
        }
        #endregion
    }
}
