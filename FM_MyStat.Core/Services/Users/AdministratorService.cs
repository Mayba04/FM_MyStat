using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserService _userService;
        private readonly IRepository<Administrator> _adminRepo;
        private readonly IMapper _mapper;

        public AdministratorService(
                UserService userService,
                IRepository<Administrator> adminRepo
            )
        {
            this._userService = userService;
            this._adminRepo = adminRepo;
        }
        #region SignIn, SignOut
        public async Task<ServiceResponse> LoginAdministratorAsync(UserLoginDTO model)
        {
            return await this._userService.LoginUserAsync(model);
        }
        public async Task<ServiceResponse> SignOutAsync()
        {
            return await this._userService.SignOutAsync();
        }
        #endregion

        #region Create admin, Delete admin, Edit password admin, Edit main info admin
        public async Task<ServiceResponse> CreateAdministratorAsync(CreateAdminDTO model)
        {
            CreateUserDTO NewUserAppUser = _mapper.Map<CreateAdminDTO, CreateUserDTO>(model);
            NewUserAppUser.Role = "Administrator";
            ServiceResponse result = await _userService.CreateUserAsync(NewUserAppUser);
            if(!result.Success)
            {
                return result;
            }
            ServiceResponse<UserDTO, object> appUserResponse = await _userService.GetUserByEmail(model.Email);
            if(appUserResponse.Success)
            {
                Administrator administrator = _mapper.Map<CreateAdminDTO, Administrator>(model);
                administrator.AppUserId = appUserResponse.Payload.Id;
                await _adminRepo.Insert(administrator);
                return new ServiceResponse(true, "Administrator was added");
            }
            return new ServiceResponse(true, "Something went wrong");
        }
        public async Task<ServiceResponse> DeleteAdministratorAsync(DeleteAdminDTO model)
        {
            await _adminRepo.Delete(model.Id);
            DeleteUserDTO deleteUserDTO = _mapper.Map<DeleteAdminDTO, DeleteUserDTO>(model);
            ServiceResponse response = await _userService.DeleteUserAsync(deleteUserDTO);
            return response;
        }
        public async Task<ServiceResponse> ChangePasswordAsync(EditUserPasswordDTO model)
        {
            return await this._userService.ChangePasswordAsync(model);
        }
        public async Task<ServiceResponse> ChangeMainInfoAdministratorAsync(EditUserDTO newinfo)
        {
            ServiceResponse serviceResponse = await this._userService.ChangeMainInfoUserAsync(newinfo);
            return serviceResponse;
        }
        #endregion
    }
}
