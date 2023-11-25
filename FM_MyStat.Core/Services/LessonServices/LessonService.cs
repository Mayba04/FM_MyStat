using AutoMapper;
using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
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

namespace FM_MyStat.Core.Services.LessonServices
{
    public class LessonService : ILessonService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Lesson> _lessonsRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<Subject> _subjectRepo;
        private readonly UserService _userService;

        public LessonService(
                IMapper mapper, 
                IRepository<Lesson> lessonRepo,
                IRepository<Teacher> teacherRepo,
                UserService userService,
                IRepository<Group> groupRepo,
                IRepository<Subject> subjectRepo
            )
        {
            _lessonsRepo = lessonRepo;
            _mapper = mapper;
            _teacherRepo = teacherRepo;
            _userService = userService;
            _groupRepo = groupRepo;
            _subjectRepo = subjectRepo;
        }

        public async Task Create(CreateLessonsDTO model)
        {
            await _lessonsRepo.Insert(_mapper.Map<CreateLessonsDTO, Lesson>(model));
            await _lessonsRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _lessonsRepo.Delete(model.Id);
            await _lessonsRepo.Save();
        }

        public async Task<LessonDTO?> Get(int id)
        {
            if (id < 0) return null;
            var category = await _lessonsRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<LessonDTO?>(category);

        }

        public async Task<List<LessonDTO>> GetAll()
        {
            List<Lesson> result = (await _lessonsRepo.GetAll()).ToList();
            List<LessonDTO> mappedLessons = result.Select(u => _mapper.Map<Lesson, LessonDTO>(u)).ToList();
            for (int i = 0; i < result.Count(); i++)
            {
                Teacher? teacher = await _teacherRepo.GetByID(mappedLessons[i].TeacherId);
                if (teacher == null)
                {
                    mappedLessons[i].Teacher = "TEACHER NOT FOUND";
                }
                else
                {
                    ServiceResponse<UserDTO, object> teacherAppUser = await _userService.GetUserById(teacher.AppUserId);
                    mappedLessons[i].Teacher = (teacherAppUser.Success) ? teacherAppUser.Payload.Email : "TEACHER NOT FOUND";
                }
                Group? group = await _groupRepo.GetByID(mappedLessons[i].GroupId);
                mappedLessons[i].Group = (group == null) ? "GROUP NOT FOUND" : group.Name;
                Subject? subject = await _subjectRepo.GetByID(mappedLessons[i].SubjectId);
                mappedLessons[i].Subject = (subject == null) ? "SUBJECT NOT FOUND" : subject.Name;
            }
            return mappedLessons;
        }

        public async Task<ServiceResponse> GetByName(LessonDTO model)
        {
            var result = await _lessonsRepo.GetItemBySpec(new LessonsSpecification.GetByName(model.Name));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Homework exists."
                };
            }
            var category = _mapper.Map<LessonDTO>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Homework successfully loaded.",
                Payload = category
            };
        }

        public async Task<LessonDTO> GetByName(string NameHomework)
        {
            var result = await _lessonsRepo.GetItemBySpec(new LessonsSpecification.GetByName(NameHomework));
            if (result != null)
            {
                LessonDTO categoryDTO = _mapper.Map<LessonDTO>(result);
                return categoryDTO;
            }
            return null;
        }

        public async Task Update(EditLessonsDTO model)
        {
            await _lessonsRepo.Update(_mapper.Map<Lesson>(model));
            await _lessonsRepo.Save();
        }

        public async Task<ServiceResponse<List<LessonDTO>, object>> GetLessonDTOByTeacher(string id)
        {
            ServiceResponse<UserDTO, object> userDTO = await _userService.GetUserById(id);
            if (userDTO.Success)
            {
                Teacher? teacher = await _teacherRepo.GetByID(userDTO.Payload.TeacherId);
                if (teacher == null)
                {
                    return new ServiceResponse<List<LessonDTO>, object>(false, "", errors: new object[] { "Lesson not found" });
                }
                IEnumerable<Lesson> lessons = await _lessonsRepo.GetListBySpec(new LessonsSpecification.GetByteacherId(teacher.Id));
                List<LessonDTO> mappedLessons = lessons.Select(u => _mapper.Map<Lesson, LessonDTO>(u)).ToList();
                return new ServiceResponse<List<LessonDTO>, object>(true, "", payload: mappedLessons);
            }
            return new ServiceResponse<List<LessonDTO>, object>(false, "", errors: new object[] { "Something went wrong" });
        }

    }
}
