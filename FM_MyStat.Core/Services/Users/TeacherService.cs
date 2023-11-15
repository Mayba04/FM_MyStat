using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services.Users
{
    public class TeacherService
    {
        private readonly UserService _userService;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IMapper _mapper;

        public TeacherService(
                UserService userService,
                IRepository<Teacher> adminRepo
            )
        {
            this._userService = userService;
            this._teacherRepo = adminRepo;
        }
        #region SignIn, SignOut
        public async Task<ServiceResponse> LoginTeacherAsync(UserLoginDTO model)
        {
            return await this._userService.LoginUserAsync(model);
        }
        public async Task<ServiceResponse> SignOutAsync()
        {
            return await this._userService.SignOutAsync();
        }
        #endregion

        #region Create admin, Delete admin, Edit password admin, Edit main info admin
        public async Task<ServiceResponse> CreateTeacherAsync(CreateTeacherDTO model)
        {
            CreateUserDTO NewUserAppUser = _mapper.Map<CreateTeacherDTO, CreateUserDTO>(model);
            NewUserAppUser.Role = "Teacher";
            ServiceResponse result = await _userService.CreateUserAsync(NewUserAppUser);
            if (!result.Success)
            {
                return result;
            }
            ServiceResponse<UserDTO, object> appUserResponse = await _userService.GetUserByEmail(model.Email);
            if (appUserResponse.Success)
            {
                Teacher teacher = _mapper.Map<CreateTeacherDTO, Teacher>(model);
                teacher.AppUserId = appUserResponse.Payload.Id;
                await _teacherRepo.Insert(teacher);
                return new ServiceResponse(true, "Teacher was added");
            }
            return new ServiceResponse(true, "Something went wrong");
        }
        public async Task<ServiceResponse> DeleteTeacherAsync(DeleteTeacherDTO model)
        {
            await _teacherRepo.Delete(model.Id);
            DeleteUserDTO deleteUserDTO = _mapper.Map<DeleteTeacherDTO, DeleteUserDTO>(model);
            ServiceResponse response = await _userService.DeleteUserAsync(deleteUserDTO);
            return response;
        }
        public async Task<ServiceResponse> ChangePasswordAsync(EditUserPasswordDTO model)
        {
            return await this._userService.ChangePasswordAsync(model);
        }
        public async Task<ServiceResponse> ChangeMainInfoAdministratorAsync(EditUserDTO newinfo)
        {
            return await this._userService.ChangeMainInfoUserAsync(newinfo);
        }
        #endregion

        public async Task<ServiceResponse<List<TeacherDTO>, object>> GetAllAsync()
        {
            ServiceResponse<List<UserDTO>, object> serviceResponse = await this._userService.GetAllAsync();
            List<UserDTO> result = (List<UserDTO>)serviceResponse.Payload.Select(u => u.Role == "Teacher");
            List<TeacherDTO> mappedUsers = result.Select(u => _mapper.Map<UserDTO, TeacherDTO>(u)).ToList();
            return new ServiceResponse<List<TeacherDTO>, object>(true, "", payload: mappedUsers);
        }

        public async Task<ServiceResponse<EditUserDTO, object>> GetEditUserDtoByIdAsync(string Id) => await this._userService.GetEditUserDtoByIdAsync(Id);
        public async Task<ServiceResponse<DeleteUserDTO, object>> GetDeleteUserDtoByIdAsync(string Id) => await this._userService.GetDeleteUserDtoByIdAsync(Id);
    }
}

