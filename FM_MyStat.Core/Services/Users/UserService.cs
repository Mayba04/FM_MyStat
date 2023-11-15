using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
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
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<ServiceResponse> LoginUserAsync(UserLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User or password incorrect."
                };
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);/// failed login user
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, model.RememberMe);

                return new ServiceResponse
                {
                    Success = true,
                    Message = "User successfully logged in."
                };
            }

            if (result.IsNotAllowed)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Confirm your email please."
                };
            }

            if (result.IsLockedOut)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User is locked. Connect with your site administrator."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "User or password incorrect."
            };
        }

        public async Task<ServiceResponse<string, object>> GetUserRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ServiceResponse<string, object>
                {
                    Success = false,
                    Message = "User not found.",
                    Errors = new[] { "UserNotFound" } 
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                return new ServiceResponse<string, object>
                {
                    Success = true,
                    Message = "User roles found.",
                    Payload = roles.FirstOrDefault() 
                };
            }

            return new ServiceResponse<string, object>
            {
                Success = false,
                Message = "User has no roles.",
                Errors = new[] { "NoRolesAssigned" } 
            };
        }
    }
}
