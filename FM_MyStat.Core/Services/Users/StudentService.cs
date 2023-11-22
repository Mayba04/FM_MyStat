using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
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
    public class StudentService
    {
        private readonly UserService _userService;
        private readonly IRepository<Student> _studentRepo;
        private readonly IRepository<Group> _groupRepo;
        private readonly IMapper _mapper;

        public StudentService(
                UserService userService,
                IRepository<Student> StudentRepo,
                IMapper mapper,
                IRepository<Group> groupRepo
            )
        {
            this._userService = userService;
            this._studentRepo = StudentRepo;
            this._mapper = mapper;
            this._groupRepo = groupRepo;
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
                await _studentRepo.Save();
                Student? studentAdd = await _studentRepo.GetItemBySpec(new StudentSpecification.GetByAppUserId(appUserResponse.Payload.Id));
                if (studentAdd != null)
                {
                    EditUserDTO editUserDTO = _mapper.Map<UserDTO, EditUserDTO>(appUserResponse.Payload);
                    editUserDTO.StudentId = studentAdd.Id;
                    ServiceResponse response = await _userService.ChangeMainInfoUserAsync(editUserDTO);
                    if (response.Success)
                    {
                        return new ServiceResponse(true, "Student was added");
                    }
                    return new ServiceResponse(false, "Something went wrong");
                }
            }
            return new ServiceResponse(true, "Something went wrong");
        }
        public async Task<ServiceResponse> DeleteStudentAsync(DeleteUserDTO model)
        {
            Student? deletestudent = await _studentRepo.GetItemBySpec(new StudentSpecification.GetByAppUserId(model.Id));
            if (deletestudent != null)
            {
                await _studentRepo.Delete(deletestudent.Id);
                await _studentRepo.Save();
                ServiceResponse response = await _userService.DeleteUserAsync(model);
                return response;
            }
            return new ServiceResponse(false, "Something went wrong");
        }
        public async Task<ServiceResponse> ChangePasswordAsync(EditUserPasswordDTO model)
        {
            return await this._userService.ChangePasswordAsync(model);
        }
        public async Task<ServiceResponse> ChangeMainInfoStudentAsync(EditUserDTO newinfo)
        {
            return await this._userService.ChangeMainInfoUserAsync(newinfo);
        }
        public async Task<ServiceResponse> EditStudentAsync(EditStudentDTO model)
        {
            EditUserDTO editUserDTO = _mapper.Map<EditStudentDTO, EditUserDTO>(model);
            ServiceResponse res = await _userService.EditUserAsync(editUserDTO);
            if (res.Success)
            {
                Student? student = await _studentRepo.GetItemBySpec(new StudentSpecification.GetByAppUserId(model.Id));
                if (student != null)
                {
                    student.Rating = model.Rating;
                    student.GroupId = model.GroupId;
                    await _studentRepo.Update(student);
                    await _studentRepo.Save();
                    return new ServiceResponse(true);
                }
                return new ServiceResponse(false, "", errors: new object[] { "Student not found" });
            }
            return res;
        }
        #endregion

        public async Task<ServiceResponse<List<StudentDTO>, object>> GetAllAsync()
        {
            ServiceResponse<List<UserDTO>, object> serviceResponse = await this._userService.GetAllAsync();
            List<UserDTO> result = serviceResponse.Payload.Where(u => u.Role == "Student").ToList();
            List<StudentDTO> mappedUsers = result.Select(u => _mapper.Map<UserDTO, StudentDTO>(u)).ToList();
            for (int i = 0; i < mappedUsers.Count(); i++)
            {
                Student student = await _studentRepo.GetByID(result[i].StudentId);
                mappedUsers[i].Rating = student.Rating;
                Group? group = await _groupRepo.GetByID(student.GroupId);
                mappedUsers[i].Group = (group == null) ? "GROUP NOT FOUND" : group.Name;
            }
            return new ServiceResponse<List<StudentDTO>, object>(true, "", payload: mappedUsers);
        }

        public async Task<ServiceResponse<EditStudentDTO, object>> GetEditUserDtoByIdAsync(string Id)
        {
            ServiceResponse<EditUserDTO, object> response = await this._userService.GetEditUserDtoByIdAsync(Id);
            if (response.Success)
            {
                Student? student = await _studentRepo.GetItemBySpec(new StudentSpecification.GetByAppUserId(response.Payload.Id));
                if (student != null)
                {
                    EditStudentDTO editStudentDTO = _mapper.Map<EditUserDTO, EditStudentDTO>(response.Payload);
                    editStudentDTO.Rating = student.Rating;
                    editStudentDTO.GroupId = student.GroupId;
                    return new ServiceResponse<EditStudentDTO, object>(true, "", payload: editStudentDTO);
                }
                return new ServiceResponse<EditStudentDTO, object>(false, "", errors: new object[] { "Student not found" });
            }
            return new ServiceResponse<EditStudentDTO, object>(response.Success, response.Message, errors:response.Errors);
        }
        public async Task<ServiceResponse<DeleteUserDTO, object>> GetDeleteUserDtoByIdAsync(string Id) => await this._userService.GetDeleteUserDtoByIdAsync(Id);
    }
}
