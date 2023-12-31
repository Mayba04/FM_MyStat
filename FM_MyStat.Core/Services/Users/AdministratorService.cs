﻿using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Specifications;
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
                IRepository<Administrator> adminRepo,
                IMapper mapper
            )
        {
            this._userService = userService;
            this._adminRepo = adminRepo;
            this._mapper = mapper;
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
        public async Task<ServiceResponse> CreateAdministratorAsync(CreateUserDTO model)
        {
            model.Role = "Administrator";
            ServiceResponse result = await _userService.CreateUserAsync(model);
            if(!result.Success)
            {
                return result;
            }
            ServiceResponse<UserDTO, object> appUserResponse = await _userService.GetUserByEmail(model.Email);
            if(appUserResponse.Success)
            {
                Administrator administrator = _mapper.Map<CreateAdminDTO, Administrator>(new CreateAdminDTO());
                administrator.AppUserId = appUserResponse.Payload.Id;
                await _adminRepo.Insert(administrator);
                await _adminRepo.Save();
                Administrator? admin = await _adminRepo.GetItemBySpec(new AdministratorSpecification.GetByAppUserId(appUserResponse.Payload.Id));
                if(admin != null)
                {
                    EditUserDTO editUserDTO = _mapper.Map<UserDTO, EditUserDTO>(appUserResponse.Payload);
                    editUserDTO.AdministratorId = admin.Id;
                    ServiceResponse response = await _userService.ChangeMainInfoUserAsync(editUserDTO);
                    if(response.Success)
                    {
                        return new ServiceResponse(true, "Administrator was added");
                    }
                    return new ServiceResponse(false, "Something went wrong");
                }
                return new ServiceResponse(false, "Something went wrong");
            }
            return new ServiceResponse(false, "Something went wrong");
        }
        public async Task<ServiceResponse> DeleteAdministratorAsync(DeleteUserDTO model)
        {
            Administrator? deletesadmin = await _adminRepo.GetItemBySpec(new AdministratorSpecification.GetByAppUserId(model.Id));
            if (deletesadmin != null)
            {
                await _adminRepo.Delete(deletesadmin.Id);
                await _adminRepo.Save();
                ServiceResponse response = await _userService.DeleteUserAsync(model);
                return response;
            }
            return new ServiceResponse(false, "Something went wrong");
        }
        public async Task<ServiceResponse> ChangePasswordAsync(EditUserPasswordDTO model)
        {
            return await this._userService.ChangePasswordAsync(model);
        }
        public async Task<ServiceResponse> ChangeMainInfoAdministratorAsync(EditUserDTO newinfo)
        {
            return await this._userService.ChangeMainInfoUserAsync(newinfo);
        }
        public async Task<ServiceResponse> EditAdministratorAsync(EditUserDTO model) => await _userService.EditUserAsync(model);
        #endregion

        public async Task<ServiceResponse<List<AdminDTO>, object>> GetAllAsync()
        {
            ServiceResponse<List<UserDTO>, object> serviceResponse = await this._userService.GetAllAsync();
            List<UserDTO> result = serviceResponse.Payload.Where(u => u.Role == "Administrator").ToList();
            List<AdminDTO> mappedUsers = result.Select(_mapper.Map<UserDTO, AdminDTO>).ToList();
            return new ServiceResponse<List<AdminDTO>, object>(true, "", payload: mappedUsers);
        }

        public async Task<ServiceResponse<EditUserDTO, object>> GetEditUserDtoByIdAsync(string Id) => await this._userService.GetEditUserDtoByIdAsync(Id);
        public async Task<ServiceResponse<DeleteUserDTO, object>> GetDeleteUserDtoByIdAsync(string Id) => await this._userService.GetDeleteUserDtoByIdAsync(Id);
    }
}
