using AutoMapper;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Specifications;
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
                IRepository<Teacher> teacherRepo,
                IMapper mapper
            )
        {
            this._userService = userService;
            this._teacherRepo = teacherRepo;
            this._mapper = mapper;
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
        public async Task<ServiceResponse> CreateTeacherAsync(CreateUserDTO model)
        {
            model.Role = "Teacher";
            ServiceResponse result = await _userService.CreateUserAsync(model);
            if (!result.Success)
            {
                return result;
            }
            ServiceResponse<UserDTO, object> appUserResponse = await _userService.GetUserByEmail(model.Email);
            if (appUserResponse.Success)
            {
                Teacher teacher = _mapper.Map<CreateTeacherDTO, Teacher>(new CreateTeacherDTO());
                teacher.AppUserId = appUserResponse.Payload.Id;
                await _teacherRepo.Insert(teacher);
                await _teacherRepo.Save();
                Teacher? teacherAdd = await _teacherRepo.GetItemBySpec(new TeacherSpecification.GetByAppUserId(appUserResponse.Payload.Id));
                if (teacherAdd != null)
                {
                    EditUserDTO editUserDTO = _mapper.Map<UserDTO, EditUserDTO>(appUserResponse.Payload);
                    editUserDTO.TeacherId = teacherAdd.Id;
                    ServiceResponse response = await _userService.ChangeMainInfoUserAsync(editUserDTO);
                    if (response.Success)
                    {
                        return new ServiceResponse(true, "Teacher was added");
                    }
                    return new ServiceResponse(false, "Something went wrong");
                }
                return new ServiceResponse(false, "Something went wrong");
            }
            return new ServiceResponse(false, "Something went wrong");
        }
        public async Task<ServiceResponse> DeleteTeacherAsync(DeleteUserDTO model)
        {
            Teacher? deleteteacher = await _teacherRepo.GetItemBySpec(new TeacherSpecification.GetByAppUserId(model.Id));
            if (deleteteacher != null)
            {
                await _teacherRepo.Delete(deleteteacher.Id);
                await _teacherRepo.Save();
                ServiceResponse response = await _userService.DeleteUserAsync(model);
                return response;
            }
            return new ServiceResponse(false, "Something went wrong");
        }
        public async Task<ServiceResponse> ChangePasswordAsync(EditUserPasswordDTO model)
        {
            return await this._userService.ChangePasswordAsync(model);
        }
        public async Task<ServiceResponse> ChangeMainInfoTeacherAsync(EditUserDTO newinfo)
        {
            return await this._userService.ChangeMainInfoUserAsync(newinfo);
        }
        public async Task<ServiceResponse> EditTeacherAsync(EditUserDTO model) => await _userService.EditUserAsync(model);
        #endregion

        public async Task<ServiceResponse<List<TeacherDTO>, object>> GetAllAsync()
        {
            ServiceResponse<List<UserDTO>, object> serviceResponse = await this._userService.GetAllAsync();
            List<UserDTO> result = serviceResponse.Payload.Where(u => u.Role == "Teacher").ToList();
            List<TeacherDTO> mappedUsers = result.Select(_mapper.Map<UserDTO, TeacherDTO>).ToList();
            return new ServiceResponse<List<TeacherDTO>, object>(true, "", payload: mappedUsers);
        }

        public async Task<ServiceResponse<EditUserDTO, object>> GetEditUserDtoByIdAsync(string Id) => await this._userService.GetEditUserDtoByIdAsync(Id);
        public async Task<ServiceResponse<DeleteUserDTO, object>> GetDeleteUserDtoByIdAsync(string Id) => await this._userService.GetDeleteUserDtoByIdAsync(Id);

        public async Task<ServiceResponse<List<GroupDTO>, object>> GetAllGroupsByIdAsync(string Id)
        {
            Teacher? teacher = await _teacherRepo.GetByID(_userService.GetUserById(Id));
            List<GroupDTO> mappedGroups = teacher.Groups.Select(u => _mapper.Map<Group, GroupDTO>(u)).ToList();
            return new ServiceResponse<List<GroupDTO>, object>(true, "", payload: mappedGroups);
        }

        public async Task<ServiceResponse<TeacherDTO, object>> GetTeacherByAppUserIdAsync(string appUserId)
        {
            Teacher teacher = await _teacherRepo.GetItemBySpec(new TeacherSpecification.GetByAppUserId(appUserId));

            if (teacher != null)
            {
                TeacherDTO mappedTeacher = _mapper.Map<Teacher, TeacherDTO>(teacher);

                return new ServiceResponse<TeacherDTO, object>(true, "", payload: mappedTeacher);
            }
            return new ServiceResponse<TeacherDTO, object>(false, "", errors: new object[] { "Teacher not found" });
        }


    }
}
