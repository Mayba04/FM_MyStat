using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.AutoMappers.Student
{
    public class AutoMapperStudentProfile : Profile
    {
        public AutoMapperStudentProfile()
        {
            CreateMap<CreateStudentDTO, Entities.Users.Student>();
            CreateMap<StudentDTO, AppUser>().ReverseMap();
            CreateMap<EditStudentDTO, AppUser>().ReverseMap();
            CreateMap<EditStudentDTO, EditUserDTO>().ReverseMap();
            CreateMap<AppUser, EditStudentDTO>().ReverseMap();
            //CreateMap<CreateStudentDTO, AppUser>().ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
        }
    }
}
