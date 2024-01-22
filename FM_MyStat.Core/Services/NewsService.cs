using AutoMapper;
using FM_MyStat.Core.DTOs.NewsDTO;
using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services
{
    internal class NewsService : INewsService
    {
        private readonly IRepository<News> _newsRepo;
        private readonly IMapper _mapper;
        public NewsService(
                IRepository<News> newsRepo,
                IMapper mapper
            )
        {
            _newsRepo = newsRepo;
            _mapper = mapper;
        }
        public async Task Create(CreateNewsDTO model)
        {
            await _newsRepo.Insert(_mapper.Map<CreateNewsDTO, News>(model));
            await _newsRepo.Save();
        }

        public async Task Delete(int id)
        {
            await _newsRepo.Delete(id);
            await _newsRepo.Save();
        }

        public async Task<NewsDTO?> Get(int id)
        {
            News news = await _newsRepo.GetByID(id);
            return (news == null) ? null : _mapper.Map<News, NewsDTO>(news);
        }

        public async Task<List<NewsDTO>> GetAll()
        {
            return (await _newsRepo.GetAll()).Select(item => _mapper.Map<News, NewsDTO>(item)).ToList();
        }

        public async Task Update(EditNewsDTO model)
        {
            await _newsRepo.Update(_mapper.Map<EditNewsDTO, News>(model));
            await _newsRepo.Save();
        }

        public async Task<EditNewsDTO?> GetEdit(int id)
        {
            News news = await _newsRepo.GetByID(id);
            return (news == null) ? null : _mapper.Map<News, EditNewsDTO>(news);
        }
    }
}
