    using AutoMapper;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services.Users;
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
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<Lesson> _lessonRepo;

        public HomeworkService(IMapper mapper, IRepository<Homework> homeRepo, IRepository<Group> groupRepo, IRepository<Lesson> lessonRepo)
        {
            _homeworkRepo = homeRepo;
            _mapper = mapper;
            _groupRepo = groupRepo;
            _lessonRepo = lessonRepo;
        }

        public async Task Create(CreateHomeworkDTO model)
        {
            await _homeworkRepo.Insert(_mapper.Map<CreateHomeworkDTO, Homework>(model));
            await _homeworkRepo.Save();
        }

        public async Task Delete(int id)
        {
            HomeworkDTO? model = await Get(id);
            if (model == null) return;
            await _homeworkRepo.Delete(id);
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
            List<HomeworkDTO> mappedHomeworks = result.Select(u => _mapper.Map<Homework, HomeworkDTO>(u)).ToList();
            for (int i = 0; i < result.Count(); i++)
            {
                Group? group = await _groupRepo.GetByID(mappedHomeworks[i].GroupId);
                mappedHomeworks[i].Group = (group == null) ? "GROUP NOT FOUND" : group.Name;
                Lesson? lesson = await _lessonRepo.GetByID(mappedHomeworks[i].LessonId);
                mappedHomeworks[i].Lesson = (lesson == null) ? "LESSON NOT FOUND" : lesson.Name;
            }
            return mappedHomeworks;
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

        public async Task<ServiceResponse<EditHomeworkDTO, object>> GetEditHomeworkDTO(int id)
        {
            Homework? homework = await _homeworkRepo.GetByID(id);
            if (homework != null)
            {
                return new ServiceResponse<EditHomeworkDTO, object>(true, "", payload: _mapper.Map<Homework, EditHomeworkDTO>(homework));
            }
            return new ServiceResponse<EditHomeworkDTO, object>(false, "", errors: new string[] { "Homework not found!" });
        }

        public async Task Update(EditHomeworkDTO model)
        {
            await _homeworkRepo.Update(_mapper.Map<Homework>(model));
            await _homeworkRepo.Save();
        }
    }
}
