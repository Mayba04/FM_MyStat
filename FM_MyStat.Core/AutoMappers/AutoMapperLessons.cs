using AutoMapper;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.DTOs.SubjectsDTO;
using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Lessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.AutoMappers
{
    public class AutoMapperLessons : Profile
    {
        public AutoMapperLessons()
        {
            CreateMap<CreateLessonsDTO, Lesson>().ReverseMap();
            CreateMap<DeleteLessonsDTO, Lesson>().ReverseMap();
            CreateMap<EditLessonsDTO, Lesson>().ReverseMap();
            CreateMap<LessonDTO, Lesson>().ReverseMap();
        }
    }
}
