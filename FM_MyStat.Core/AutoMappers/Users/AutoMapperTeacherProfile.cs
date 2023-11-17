using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.AutoMappers.Users
{
    public class AutoMapperTeacherProfile : Profile
    {
        public AutoMapperTeacherProfile()
        {
            CreateMap<TeacherDTO, AppUser>().ReverseMap();
            CreateMap<EditTeacherDTO, AppUser>().ReverseMap();
            CreateMap<AppUser, EditTeacherDTO>().ReverseMap();
            CreateMap<CreateTeacherDTO, AppUser>().ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
            CreateMap<CreateTeacherDTO, Teacher>().ReverseMap();
        }
    }
}
