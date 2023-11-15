using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.AutoMappers.Users
{
    internal class AutoMapperAdminProfile : Profile
    {
        public AutoMapperAdminProfile()
        {
            CreateMap<AdminDTO, AppUser>().ReverseMap();
            CreateMap<EditAdminDTO, AppUser>().ReverseMap();
            CreateMap<EditAdminDTO, AppUser>().ReverseMap();
            CreateMap<CreateAdminDTO, AppUser>().ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
        }
    }
}
