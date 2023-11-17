using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services.Users
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(
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
        public async Task<ServiceResponse> LoginUserAsync(UserLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse(false, "User or password incorrect.");
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);/// failed login user
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, model.RememberMe);

                return new ServiceResponse(true, "User successfully logged in.");
            }
            if (result.IsNotAllowed)
            {
                return new ServiceResponse(false, "Confirm your email please.");
            }
            if (result.IsLockedOut)
            {
                return new ServiceResponse(false, "User is locked. Connect with your site administrator.");
            }
            return new ServiceResponse(false, "User or password incorrect.");
        }
        public async Task<ServiceResponse> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return new ServiceResponse(true, "false");
        }
        #endregion

        #region Get users mapped
        public async Task<ServiceResponse<AppUser?, string>>GetAppUserByEmail(string email)
        {
            AppUser? user = await this._userManager.FindByEmailAsync(email);
            return (user != null) ?
                new ServiceResponse<AppUser?, string>(true, "User succesfully loaded", user) :
                new ServiceResponse<AppUser?, string>(false, "User not found");
        }
        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }
        private async Task<ServiceResponse<Mapped, object>> GetMappedUserByIdAsync<Mapped>(string Id)
        {
            AppUser? user = await this._userManager.FindByIdAsync(Id);
            return (user != null) ?
                new ServiceResponse<Mapped, object>(true, "User succesfully loaded", payload: _mapper.Map<AppUser, Mapped>(user)) :
                new ServiceResponse<Mapped, object>(false, "User not found");
        }
        public async Task<ServiceResponse<List<UserDTO>, object>> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<UserDTO> mappedUsers = users.Select(u => _mapper.Map<AppUser, UserDTO>(u)).ToList();

            for (int i = 0; i < mappedUsers.Count; i++)
            {
                mappedUsers[i].Role = string.Join(", ", _userManager.GetRolesAsync(users[i]).Result);
            }

            return new ServiceResponse<List<UserDTO>, object>(true, "All Users loaded", payload: mappedUsers);
        }
        public async Task<ServiceResponse<DeleteUserDTO, object>> GetDeleteUserDtoByIdAsync(string Id) => this.GetMappedUserByIdAsync<DeleteUserDTO>(Id).Result;
        public async Task<ServiceResponse<EditUserDTO, object>> GetEditUserDtoByIdAsync(string Id) => this.GetMappedUserByIdAsync<EditUserDTO>(Id).Result;
        public async Task<ServiceResponse<UserDTO, object>>GetUserByEmail(string email)
        {
            AppUser? user = await this._userManager.FindByEmailAsync(email);
            return (user != null) ?
                new ServiceResponse<UserDTO, object>(true, "User succesfully loaded", payload: _mapper.Map<AppUser, UserDTO>(user)) :
                new ServiceResponse<UserDTO, object>(false, "User not found");
        }
        #endregion

        public async Task<ServiceResponse> GetUserRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ServiceResponse(false, "User not found.", new[] { "UserNotFound" });
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                return new ServiceResponse(true, "User roles found.", roles.FirstOrDefault());
            }
            return new ServiceResponse(false, "User has no roles.", new[] { "NoRolesAssigned" });
        }

        #region Create user, Delete user, Edit password user, Edit main info user
        public async Task<ServiceResponse> CreateUserAsync(CreateUserDTO model)
        {
            AppUser NewUser = _mapper.Map<CreateUserDTO, AppUser>(model);
            IdentityResult result = await _userManager.CreateAsync(NewUser, "Qwerty-1");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(NewUser, model.Role);
                await SendConfirmationEmailAsync(NewUser);
                return new ServiceResponse(true, "User has been added");
            }
            return new ServiceResponse(false, "Something went wrong", errors: result.Errors.Select(e => e.Description));
        }
        public async Task<ServiceResponse> DeleteUserAsync(DeleteUserDTO model)
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
        public async Task<ServiceResponse> ChangePasswordAsync(EditUserPasswordDTO model)
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
        public async Task<ServiceResponse> ChangeMainInfoUserAsync(EditUserDTO newinfo)
        {
            AppUser user = await _userManager.FindByIdAsync(newinfo.Id);

            if (user != null)
            {
                user.FirstName = newinfo.FirstName;
                user.LastName = newinfo.LastName;
                user.Email = newinfo.Email;
                user.PhoneNumber = newinfo.PhoneNumber;
                user.AdministratorId = newinfo.AdministratorId;
                user.TeacherId = newinfo.TeacherId;
                user.StudentId = newinfo.StudentId;

                IdentityResult result = await _userManager.UpdateAsync(user);

                return (result.Succeeded) ?
                    new ServiceResponse(true, "Information has been changed") :
                    new ServiceResponse(false, "Something went wrong", errors: result.Errors.Select(e => e.Description));
            }
            return new ServiceResponse(false, "Not found user");
        }
        public async Task<ServiceResponse> EditUserAsync(EditUserDTO model)
        {
            AppUser? user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return new ServiceResponse(false, "User not found.", errors: new List<string>() { "User not found." });
            }

            if (user.Email != model.Email)
            {
                user.EmailConfirmed = false;
                user.Email = model.Email;
                user.UserName = model.Email;
                await SendConfirmationEmailAsync(user);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ServiceResponse(true, "User successfully updated.");
            }
            return new ServiceResponse(false, "Something went wrong", errors: result.Errors.Select(e => e.Description));
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
            await _emailService.SendEmailAsync(user.Email, "Email confirmation.", emailBody);
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

        #region Password recovery and send token for password recovery
        public async Task<ServiceResponse> ForgotPasswordAsync(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ServiceResponse(false, "User not found.");
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] encodedToken = Encoding.UTF8.GetBytes(token);
            string validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["HostSettings:URL"]}/Dashboard/ResetPassword?email={email}&token={validEmailToken}";

            string emailBody = $"<h1>Follow the instruction for reset password.</h1><a href='{url}'>Reset now!</a>";
            await _emailService.SendEmailAsync(email, "Reset password for TopNews.", emailBody);

            return new ServiceResponse(true, "Email successfull send.");
        }

        public async Task<ServiceResponse> ResetPasswordAsync(PasswordRecoveryDto model)
        {
            AppUser? user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse(false, "User not found.");
            }

            byte[] decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string narmalToken = Encoding.UTF8.GetString(decodedToken);
            IdentityResult res = await _userManager.ResetPasswordAsync(user, narmalToken, model.Password);
            if (res.Succeeded)
            {
                return new ServiceResponse(true, "Password changed successfully");
            }
            return new ServiceResponse(false, "Something went wrong", errors: res.Errors.Select(e => e.Description));
        }
        #endregion
    }
}
