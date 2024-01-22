using AutoMapper;
using FM_MyStat.Core.DTOs.NewsDTO;
using FM_MyStat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.AutoMappers
{
    public class AutoMapperNewsProfile : Profile
    {
        public AutoMapperNewsProfile()
        {
            CreateMap<News, NewsDTO>().ReverseMap();
            CreateMap<News, CreateNewsDTO>().ReverseMap();
            CreateMap<News, EditNewsDTO>().ReverseMap();
            CreateMap<News, DeleteNewsDTO>().ReverseMap();
        }
    }
}
