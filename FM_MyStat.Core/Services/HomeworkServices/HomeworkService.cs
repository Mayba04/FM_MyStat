using AutoMapper;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public HomeworkService(IMapper mapper, IRepository<Homework> homeRepo, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _homeworkRepo = homeRepo;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task Create(HomeworkDTO model)
        {
            if (model.File.Count > 0)
            {
                string wevRootPath = _webHostEnvironment.WebRootPath;
                string uploadt = wevRootPath + _configuration.GetValue<string>("FileSettings:FilePath");
                var files = model.File;
                var fileName = Guid.NewGuid().ToString();
                string extansions = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploadt, fileName + extansions), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                model.PathFile = fileName + extansions;
            }
            else
            {
                model.PathFile = "Default.txt";
            }
           
            var homeworkEntity = _mapper.Map<Homework>(model);
            homeworkEntity.Lesson = null;
            homeworkEntity.Group = null;
            homeworkEntity.HomeworksDone = null;
            await _homeworkRepo.Insert(homeworkEntity);
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
            var homework = await _homeworkRepo.GetByID(id);
            if (homework == null) return null;
            return _mapper.Map<HomeworkDTO?>(homework);

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
