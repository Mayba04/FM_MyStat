using AutoMapper;
using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.Entities.Homeworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.AutoMappers
{
    public class AutoMapperHomeworkDone : Profile
    {
        public AutoMapperHomeworkDone()
        {
            CreateMap<HomeworkDone, HomeworkDoneDTO>().ReverseMap();
        }
    }
}
