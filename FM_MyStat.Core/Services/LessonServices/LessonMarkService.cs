﻿using AutoMapper;
using FM_MyStat.Core.DTOs.LessonsDTO.LessonMark;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
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

        public LessonMarkService(IMapper mapper, IRepository<LessonMark> lessonMarkRepo, IRepository<Lesson> lessonRepo, IRepository<Student> studentRepo)
        {
            _lessonsMarkRepo = lessonMarkRepo;
            _mapper = mapper;
            _lessonRepo = lessonRepo;
            _studentRepo = studentRepo;
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

        public async Task AddGrade(LessonMarkDTO model)
        {
            // Check if the lesson and student exist
            var lesson = _lessonRepo.GetByID(model.LessonId).Result;
            var student = _studentRepo.GetByID(model.StudentId).Result;

            // Add the lesson mark
            var lessonMarkEntity = _mapper.Map<LessonMark>(model);
            _lessonsMarkRepo.Insert(lessonMarkEntity).Wait();
            _lessonsMarkRepo.Save().Wait();

            // Update the student's overall rating
            UpdateStudentRating(model.StudentId).Wait();
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
    }
}
