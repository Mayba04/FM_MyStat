using AutoMapper;
using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services
{
    public class HomeworkDoneService: IHomeworkDoneService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<HomeworkDone> _homeworkDoneRepo;

        public HomeworkDoneService(IMapper mapper, IRepository<HomeworkDone> homeRepo)
        {
            _homeworkDoneRepo = homeRepo;
            _mapper = mapper;
        }

        public async Task Create(HomeworkDoneDTO model)
        {
            await _homeworkDoneRepo.Insert(_mapper.Map<HomeworkDone>(model));
            await _homeworkDoneRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _homeworkDoneRepo.Delete(model.Id);
            await _homeworkDoneRepo.Save();
        }

        public async Task<HomeworkDoneDTO?> Get(int id)
        {
            if (id < 0) return null;
            var category = await _homeworkDoneRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<HomeworkDoneDTO?>(category);

        }

        public async Task<List<HomeworkDoneDTO>> GetAll()
        {
            var result = await _homeworkDoneRepo.GetAll();
            return _mapper.Map<List<HomeworkDoneDTO>>(result);
        }

        public async Task<ServiceResponse> GetByName(HomeworkDoneDTO model)
        {
            var result = await _homeworkDoneRepo.GetItemBySpec(new HomeworkDoneSpecification.GetByDescription(model.Description));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "HomeworkDone exists."
                };
            }
            var category = _mapper.Map<HomeworkDoneDTO>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "HomeworkDone successfully loaded.",
                Payload = category
            };
        }

        public async Task<HomeworkDoneDTO> GetByName(string Description)
        {
            var result = await _homeworkDoneRepo.GetItemBySpec(new HomeworkDoneSpecification.GetByDescription(Description));
            if (result != null)
            {
                HomeworkDoneDTO categoryDTO = _mapper.Map<HomeworkDoneDTO>(result);
                return categoryDTO;
            }
            return null;
        }

        public async Task Update(HomeworkDoneDTO model)
        {
            await _homeworkDoneRepo.Update(_mapper.Map<HomeworkDone>(model));
            await _homeworkDoneRepo.Save();
        }
    }
}
