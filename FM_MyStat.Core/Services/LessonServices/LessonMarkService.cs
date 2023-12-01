using AutoMapper;
using FM_MyStat.Core.DTOs.LessonsDTO.LessonMark;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
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
    public class LessonMarkService : ILessonMarkService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<LessonMark> _lessonsMarkRepo;
        private readonly IRepository<Student> _studentRepo;
        private readonly IRepository<Lesson> _lessonRepo;
        private readonly UserService _userService;

        public LessonMarkService(UserService userService, IMapper mapper, IRepository<LessonMark> lessonMarkRepo, IRepository<Lesson> lessonRepo, IRepository<Student> studentRepo)
        {
            _lessonsMarkRepo = lessonMarkRepo;
            _mapper = mapper;
            _lessonRepo = lessonRepo;
            _studentRepo = studentRepo;
            _userService = userService;
        }

        public async Task Create(CreateLessonMarkDTO model)
        {
            await _lessonsMarkRepo.Insert(_mapper.Map<LessonMark>(model));
            await _lessonsMarkRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _lessonsMarkRepo.Delete(model.Id);
            await _lessonsMarkRepo.Save();
        }

        public async Task<LessonMarkDTO?> Get(int id)
        {
            if (id < 0) return null;
            var category = await _lessonsMarkRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<LessonMarkDTO?>(category);

        }

        public async Task<List<LessonMarkDTO>> GetAll()
        {
            var result = await _lessonsMarkRepo.GetAll();
            return _mapper.Map<List<LessonMarkDTO>>(result);
        }

        public async Task<ServiceResponse> GetById(LessonMarkDTO model)
        {
            var result = await _lessonsMarkRepo.GetItemBySpec(new LessonsMarkSpecification.GetById(model.Id));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Homework exists."
                };
            }
            var category = _mapper.Map<LessonMarkDTO>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Homework successfully loaded.",
                Payload = category
            };
        }

        public async Task<LessonMarkDTO> GetById(int Id)
        {
            var result = await _lessonsMarkRepo.GetItemBySpec(new LessonsMarkSpecification.GetById(Id));
            if (result != null)
            {
                LessonMarkDTO categoryDTO = _mapper.Map<LessonMarkDTO>(result);
                return categoryDTO;
            }
            return null;
        }


        public async Task Update(EditLessonMarkDTO model)
        {
            await _lessonsMarkRepo.Update(_mapper.Map<LessonMark>(model));
            await _lessonsMarkRepo.Save();
        }

        private async Task UpdateStudentRating(int studentId)
        {
            var lessonMarks = await _lessonsMarkRepo.GetListBySpec(new LessonsMarkSpecification.GetByStudentId(studentId));
            if (lessonMarks.Any())
            {
                double overallRating = lessonMarks.Average(mark => mark.Mark);
                var student = await _studentRepo.GetByID(studentId);
                if (student != null)
                {
                    student.Rating = Convert.ToInt32(overallRating);
                    await _studentRepo.Update(student);
                    await _studentRepo.Save();
                }
            }
        }

        public async Task<List<StudentDTO>> GetAllStudents()
        {
            var students = await _studentRepo.GetAll();
            return _mapper.Map<List<StudentDTO>>(students);
        }

        public async Task<List<StudentMarkDTO>> GetAllStudentsByLesson(int lessonId)
        {
            Lesson lesson = await _lessonRepo.GetByID(lessonId);
            List<Student> students = (await _studentRepo.GetListBySpec(new StudentSpecification.GetByGroupId(lesson.GroupId))).ToList();
            List<StudentMarkDTO> studentsList = students.Select(p => _mapper.Map<StudentMarkDTO>(p)).ToList();
            for (int i = 0; i < studentsList.Count(); i++)
            {
                ServiceResponse<AppUser, string> app = await _userService.GetAppUserById(students[i].AppUserId);
                LessonMark lessonMark = await _lessonsMarkRepo.GetItemBySpec(new LessonsMarkSpecification.GetByLessonId(lessonId, (int)app.Payload.StudentId));
                if (lessonMark == null)
                {
                    LessonMark lessonMark1 = new LessonMark();
                    lessonMark1.LessonId = lessonId;
                    lessonMark1.StudentId = (int)app.Payload.StudentId;
                    lessonMark1.Mark = 1;
                    await _lessonsMarkRepo.Insert(lessonMark1);
                    await _lessonsMarkRepo.Save();
                    studentsList[i].Mark = lessonMark1.Mark;
                    studentsList[i].LessonMarkId = lessonMark1.Id;
                }
                else
                {
                    studentsList[i].Mark = lessonMark.Mark;
                    studentsList[i].LessonMarkId = lessonMark.Id;
                }
                studentsList[i].FirstName = (string)app.Payload.FirstName;
                studentsList[i].LastName = (string)app.Payload.LastName;
                studentsList[i].SurName = (string)app.Payload.SurName;
            }
            return studentsList;
        }

        public async Task SetMarksStudents(List<StudentMarkDTO> studentMarks)
        {
            foreach (StudentMarkDTO studentMark in studentMarks)
            {
                LessonMark lessonMark = await _lessonsMarkRepo.GetByID(studentMark.LessonMarkId);
                lessonMark.Mark = studentMark.Mark;
                await _lessonsMarkRepo.Update(lessonMark);
                await _lessonsMarkRepo.Save();
                await UpdateStudentRating((int)studentMark.StudentId);
            }
        }
    }
}
