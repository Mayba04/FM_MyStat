using AutoMapper;
using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services
{
    public class LessonsService: ILessonsService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Lesson> _lessonskRepo;

        public LessonsService(IMapper mapper, IRepository<Lesson> lessonRepo)
        {
            _lessonskRepo = lessonRepo;
            _mapper = mapper;
        }

        public async Task Create(CreateLessonsDTO model)
        {
            await _lessonskRepo.Insert(_mapper.Map<Lesson>(model));
            await _lessonskRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _lessonskRepo.Delete(model.Id);
            await _lessonskRepo.Save();
        }

        public async Task<LessonsDTO?> Get(int id)
        {
            if (id < 0) return null;
            var category = await _lessonskRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<LessonsDTO?>(category);

        }

        public async Task<List<LessonsDTO>> GetAll()
        {
            var result = await _lessonskRepo.GetAll();
            return _mapper.Map<List<LessonsDTO>>(result);
        }

        public async Task<ServiceResponse> GetByName(LessonsDTO model)
        {
            var result = await _lessonskRepo.GetItemBySpec(new LessonsSpecification.GetByName(model.Name));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Homework exists."
                };
            }
            var category = _mapper.Map<LessonsDTO>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Homework successfully loaded.",
                Payload = category
            };
        }

        public async Task<LessonsDTO> GetByName(string NameHomework)
        {
            var result = await _lessonskRepo.GetItemBySpec(new LessonsSpecification.GetByName(NameHomework));
            if (result != null)
            {
                LessonsDTO categoryDTO = _mapper.Map<LessonsDTO>(result);
                return categoryDTO;
            }
            return null;
        }

        public Task<LessonsDTO> GetByName(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(EditLessonsDTO model)
        {
            await _lessonskRepo.Update(_mapper.Map<Lesson>(model));
            await _lessonskRepo.Save();
        }

        
    }
}
