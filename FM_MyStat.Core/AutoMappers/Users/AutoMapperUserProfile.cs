using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.AutoMappers.Users
{
    public class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile()
        {
            CreateMap<UserDTO, AppUser>().ReverseMap();
            CreateMap<EditUserDTO, AppUser>().ReverseMap();
            CreateMap<CreateUserDTO, AppUser>().ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
            CreateMap<CreateUserDTO, AppUser>();
            CreateMap<DeleteUserDTO, AppUser>().ReverseMap();

            /// ......
            CreateMap<CreateAdminDTO, CreateUserDTO>().ReverseMap();
            CreateMap<CreateTeacherDTO, CreateUserDTO>().ReverseMap();
            CreateMap<CreateStudentDTO, CreateUserDTO>().ReverseMap();
        }
    }
}
