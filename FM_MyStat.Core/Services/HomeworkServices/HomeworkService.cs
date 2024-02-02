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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = FM_MyStat.Core.Entities.Group;

namespace FM_MyStat.Core.Services.HomeworkServices
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Homework> _homeworkRepo;
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<Lesson> _lessonRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly StudentService _studentService;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly UserService _userService;

        public HomeworkService(
            StudentService studentService,
            IMapper mapper,
            IRepository<Homework> homeRepo,
            IRepository<Group> groupRepo,
            IRepository<Lesson> lessonRepo,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IRepository<Teacher> teacherRepo,
            UserService userService
            )
        {
            _homeworkRepo = homeRepo;
            _mapper = mapper;
            _groupRepo = groupRepo;
            _lessonRepo = lessonRepo;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _studentService = studentService;
            _teacherRepo = teacherRepo;
            _userService = userService;
        }

        public async Task Create(CreateHomeworkDTO model)
        {
            if (model.File != null)
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
            Homework addedHomework = _mapper.Map<CreateHomeworkDTO, Homework>(model);
            Lesson lesson = await _lessonRepo.GetByID(model.LessonId);
            Group group = await _groupRepo.GetByID(lesson.GroupId);
            addedHomework.GroupId = group.Id;
            await _homeworkRepo.Insert(addedHomework);
            await _homeworkRepo.Save();
            lesson.HomeworkId = addedHomework.Id;
            await _lessonRepo.Update(lesson);
            await _lessonRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            string webPathRoot = _webHostEnvironment.WebRootPath;
            string upload = webPathRoot + _configuration.GetValue<string>("FileSettings:FilePath");
            string existingFilePath = Path.Combine(upload, model.PathFile);

            if (File.Exists(existingFilePath) && model.PathFile != "Default.txt")
            {
                File.Delete(existingFilePath);
            }

            Lesson lesson = await _lessonRepo.GetByID(model.LessonId);
            if (lesson != null)
            {
                lesson.HomeworkId = null;
                await _lessonRepo.Update(lesson);
                await _lessonRepo.Save();
            }
           
           
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
            List<Homework> result = (await _homeworkRepo.GetAll()).ToList();
            List<HomeworkDTO> mappedHomeworks = await UploadDataFromDatabaseForHomeworkDTOAsync(result);
            return mappedHomeworks;
        }

        private async Task<List<HomeworkDTO>> UploadDataFromDatabaseForHomeworkDTOAsync(List<Homework> homeworks)
        {
            List<HomeworkDTO> mappedHomeworks = homeworks.Select(u => _mapper.Map<Homework, HomeworkDTO>(u)).ToList();
            for (int i = 0; i < homeworks.Count(); i++)
            {
                Group? group = await _groupRepo.GetByID(mappedHomeworks[i].GroupId);
                mappedHomeworks[i].Group = (group == null) ? "GROUP NOT FOUND" : group.Name;
                Lesson? lesson = await _lessonRepo.GetByID(mappedHomeworks[i].LessonId);
                mappedHomeworks[i].Lesson = (lesson == null) ? "LESSON NOT FOUND" : lesson.Name;
            }
            return mappedHomeworks;
        }

        public async Task<List<HomeworkDTO>> GetByTeacherId(string Id)
        {
            ServiceResponse<UserDTO, object> userDTO = await _userService.GetUserById(Id);
            if (userDTO.Success)
            {
                Teacher? teacher = await _teacherRepo.GetByID(userDTO.Payload.TeacherId);
                if (teacher == null)
                {
                    return new List<HomeworkDTO> { };
                }
                List<Homework> homeworks = (await _homeworkRepo.GetListBySpec(new HomeworkSpecification.GetByTeacherId(teacher.Id))).ToList();
                List<HomeworkDTO> mappedHomeworks = await this.UploadDataFromDatabaseForHomeworkDTOAsync(homeworks);
                return mappedHomeworks;
            }
            return new List<HomeworkDTO> { };
        }



        public async Task<List<HomeworkDTO>> GetAllByUserId(string studentId)
        {
            var student = await _studentService.GetEditUserDtoByIdAsync(studentId);
            if (student?.Payload?.GroupId == null)
            {
                return new List<HomeworkDTO>();
            }
            var allHomeworks = await _homeworkRepo.GetAll();
            var filteredHomeworks = allHomeworks.Where(h => h.GroupId == student.Payload.GroupId).ToList();
            
            List<HomeworkDTO> mappedHomeworks = filteredHomeworks.Select(u => _mapper.Map<Homework, HomeworkDTO>(u)).ToList();
            for (int i = 0; i < filteredHomeworks.Count(); i++)
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

        public async Task<ServiceResponse<CreateHomeworkDTO, object>> GetCreateHomeworkDTO(int id)
        {
            Homework? homework = await _homeworkRepo.GetByID(id);
            if (homework != null)
            {
                return new ServiceResponse<CreateHomeworkDTO, object>(true, "", payload: _mapper.Map<Homework, CreateHomeworkDTO>(homework));
            }
            return new ServiceResponse<CreateHomeworkDTO, object>(false, "", errors: new string[] { "Homework not found!" });
        }

        private async Task<CreateHomeworkDTO> GGGGG4(CreateHomeworkDTO model)
        {
            var currentHomework = await _homeworkRepo.GetByID(model.Id);
            if (model.File != null)
            {
                string webPathRoot = _webHostEnvironment.WebRootPath;
                string upload = webPathRoot + _configuration.GetValue<string>("FileSettings:FilePath");

                string existingFilePath = Path.Combine(upload, currentHomework.PathFile);

                if (File.Exists(existingFilePath) && model.PathFile != "Default.png")
                {
                    File.Delete(existingFilePath);
                }

                var files = model.File;

                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                model.PathFile = fileName + extension;
                return model;
            }
            else
            {
                model.PathFile = currentHomework.PathFile;
                return model;
            }
        }


        public async Task Update(CreateHomeworkDTO model)
        {
            Homework homeworkEntity = _mapper.Map<CreateHomeworkDTO, Homework>(await GGGGG4(model));
            await _homeworkRepo.Update(homeworkEntity);
            await _homeworkRepo.Save();
        }

        public async Task<(byte[] fileContents, string contentType, string fileName)> DownloadHomeworkFileAsync(int homeworkId)
        {
            var homework = await _homeworkRepo.GetByID(homeworkId);
            if (homework == null)
            {
                return (null, null, null); 
            }

            string webPathRoot = _webHostEnvironment.WebRootPath;
            string upload = webPathRoot + _configuration.GetValue<string>("FileSettings:FilePath");

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, upload, homework.PathFile);

            filePath = Path.GetFullPath(filePath);


            if (!System.IO.File.Exists(filePath))
            {
                return (null, null, null); 
            }

            byte[] fileContents = await System.IO.File.ReadAllBytesAsync(filePath);
            string contentType = "application/octet-stream"; 
            string fileName = Path.GetFileName(filePath);

            return (fileContents, contentType, fileName);
        }

    }
}
