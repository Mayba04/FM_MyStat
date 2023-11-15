using AutoMapper;
using FM_MyStat.Core.DTOs.LessonsDTO.LessonMark;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.Entities.Lessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.AutoMappers
{

    public class AutoMapperLessonMark : Profile
    {
        public AutoMapperLessonMark()
        {
            CreateMap<CreateLessonMarkDTO, LessonMark>().ReverseMap();
            CreateMap<DeleteLessonMarkDTO, LessonMark>().ReverseMap();
            CreateMap<EditLessonMarkDTO, LessonMark>().ReverseMap();
            CreateMap<LessonMarkDTO, LessonMark>().ReverseMap();
        }
    }
}
