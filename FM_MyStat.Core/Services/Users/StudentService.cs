using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
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
    public class StudentService
    {
        private readonly UserService _userService;
        private readonly IRepository<Student> _studentRepo;
        private readonly IMapper _mapper;

        public StudentService(
                UserService userService,
                IRepository<Student> StudentRepo,
                IMapper mapper
            )
        {
            this._userService = userService;
            this._studentRepo = StudentRepo;
            this._mapper = mapper;
        }
        #region SignIn, SignOut
        public async Task<ServiceResponse> LoginStudentAsync(UserLoginDTO model)
        {
            return await this._userService.LoginUserAsync(model);
        }
        public async Task<ServiceResponse> SignOutAsync()
        {
            return await this._userService.SignOutAsync();
        }
        #endregion

        #region Create Student, Delete Student, Edit password Student, Edit main info Student
        public async Task<ServiceResponse> CreateStudentAsync(CreateStudentDTO model)
        {
            CreateUserDTO NewUserAppUser = _mapper.Map<CreateStudentDTO, CreateUserDTO>(model);
            NewUserAppUser.Role = "Student";
            ServiceResponse result = await _userService.CreateUserAsync(NewUserAppUser);
            if (!result.Success)
            {
                return result;
            }
            ServiceResponse<UserDTO, object> appUserResponse = await _userService.GetUserByEmail(model.Email);
            if (appUserResponse.Success)
            {
                Student student = _mapper.Map<CreateStudentDTO, Student>(model);
                student.AppUserId = appUserResponse.Payload.Id;
                await _studentRepo.Insert(student);
                return new ServiceResponse(true, "Student was added");
            }
            return new ServiceResponse(true, "Something went wrong");
        }
        public async Task<ServiceResponse> DeleteStudentAsync(DeleteStudentDTO model)
        {
            await _studentRepo.Delete(model.Id);
            DeleteUserDTO deleteUserDTO = _mapper.Map<DeleteStudentDTO, DeleteUserDTO>(model);
            ServiceResponse response = await _userService.DeleteUserAsync(deleteUserDTO);
            return response;
        }
        public async Task<ServiceResponse> ChangePasswordAsync(EditUserPasswordDTO model)
        {
            return await this._userService.ChangePasswordAsync(model);
        }
        public async Task<ServiceResponse> ChangeMainInfoStudentAsync(EditUserDTO newinfo)
        {
            return await this._userService.ChangeMainInfoUserAsync(newinfo);
        }
        #endregion

        public async Task<ServiceResponse<List<StudentDTO>, object>> GetAllAsync()
        {
            ServiceResponse<List<UserDTO>, object> serviceResponse = await this._userService.GetAllAsync();
            List<UserDTO> result = serviceResponse.Payload.Where(u => u.Role == "Student").ToList();
            List<StudentDTO> mappedUsers = result.Select(u => _mapper.Map<UserDTO, StudentDTO>(u)).ToList();
            return new ServiceResponse<List<StudentDTO>, object>(true, "", payload: mappedUsers);
        }

        public async Task<ServiceResponse<EditUserDTO, object>> GetEditUserDtoByIdAsync(string Id) => await this._userService.GetEditUserDtoByIdAsync(Id);
        public async Task<ServiceResponse<DeleteUserDTO, object>> GetDeleteUserDtoByIdAsync(string Id) => await this._userService.GetDeleteUserDtoByIdAsync(Id);
    }
}
