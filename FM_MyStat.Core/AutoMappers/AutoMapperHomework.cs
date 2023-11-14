using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.Entities.Homeworks;

namespace FM_MyStat.Core.AutoMappers
{
    public class AutoMapperHomework : Profile
    {
        public AutoMapperHomework()
        {
            CreateMap<Homework, HomeworkDTO>().ReverseMap();
        }
    }
}
