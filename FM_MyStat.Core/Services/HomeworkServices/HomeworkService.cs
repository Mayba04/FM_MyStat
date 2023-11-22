using AutoMapper;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services.HomeworkServices
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Homework> _homeworkRepo;

        public HomeworkService(IMapper mapper, IRepository<Homework> homeRepo)
        {
            _homeworkRepo = homeRepo;
            _mapper = mapper;
        }

        public async Task Create(CreateHomeworkDTO model)
        {
            await _homeworkRepo.Insert(_mapper.Map<Homework>(model));
            await _homeworkRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _homeworkRepo.Delete(model.Id);
            await _homeworkRepo.Save();
        }

        public async Task<HomeworkDTO?> Get(int id)
        {
            if (id < 0) return null;
            var category = await _homeworkRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<HomeworkDTO?>(category);

        }

        public async Task<List<HomeworkDTO>> GetAll()
        {
            var result = await _homeworkRepo.GetAll();
            return _mapper.Map<List<HomeworkDTO>>(result);
        }

        public async Task<ServiceResponse> GetByName(HomeworkDTO model)
        {
            var result = await _homeworkRepo.GetItemBySpec(new HomeworkSpecification.GetByTitle(model.Title));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Homework exists."
                };
            }
            var category = _mapper.Map<HomeworkDTO>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Homework successfully loaded.",
                Payload = category
            };
        }

        public async Task<HomeworkDTO> GetByName(string NameHomework)
        {
            var result = await _homeworkRepo.GetItemBySpec(new HomeworkSpecification.GetByTitle(NameHomework));
            if (result != null)
            {
                HomeworkDTO categoryDTO = _mapper.Map<HomeworkDTO>(result);
                return categoryDTO;
            }
            return null;
        }

        public async Task Update(EditHomeworkDTO model)
        {
            await _homeworkRepo.Update(_mapper.Map<Homework>(model));
            await _homeworkRepo.Save();
        }
    }
}
