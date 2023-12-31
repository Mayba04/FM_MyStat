﻿using AutoMapper;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.SubjectsDTO;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Subject> _subjectRepo;
        private readonly UserService _userService;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<TeacherSubject> _teachersubjectRepo;

        public SubjectService(IMapper mapper, IRepository<Subject> subjectRepo, UserService userService, IRepository<Teacher> teacherRepo, IRepository<TeacherSubject> teachersubjectRepo)
        {
            _subjectRepo = subjectRepo;
            _mapper = mapper;
            this._userService = userService;
            this._teacherRepo = teacherRepo;
            this._teachersubjectRepo = teachersubjectRepo;
        }

        public async Task Create(CreateSubjectDTO model)
        {
            await _subjectRepo.Insert(_mapper.Map<Subject>(model));
            await _subjectRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _subjectRepo.Delete(model.Id);
            await _subjectRepo.Save();
        }

        public async Task<SubjectDTO?> Get(int id)
        {
            if (id < 0) return null;
            var subject = await _subjectRepo.GetByID(id);
            if (subject == null) return null;
            return _mapper.Map<SubjectDTO?>(subject);
        }
        public async Task<EditSubjectDTO?> GetEditSubjectDTO(int id)
        {
            if (id < 0) return null;
            var subject = await _subjectRepo.GetByID(id);
            if (subject == null) return null;
            return _mapper.Map<EditSubjectDTO?>(subject);
        }

        public async Task<List<SubjectDTO>> GetAll()
        {
            var result = await _subjectRepo.GetAll();
            return _mapper.Map<List<SubjectDTO>>(result);
        }

        public async Task<ServiceResponse> GetByName(SubjectDTO model)
        {
            var result = await _subjectRepo.GetItemBySpec(new SubjectSpecification.GetByName(model.Name));
            if (result != null)
            {
                return new ServiceResponse(false, "Subject exists.");
            }
            var subject = _mapper.Map<SubjectDTO>(result);
            return new ServiceResponse(true, "Subject successfully loaded.", payload: subject);
        }

        public async Task<SubjectDTO> GetByName(string NameSubject)
        {
            var result = await _subjectRepo.GetItemBySpec(new SubjectSpecification.GetByName(NameSubject));
            if (result != null)
            {
                SubjectDTO subjectDTO = _mapper.Map<SubjectDTO>(result);
                return subjectDTO;
            }
            return null;
        }

        public async Task<ServiceResponse<List<SubjectDTO>, object>> GetSubjectDTOByTeacher(string id)
        {
            ServiceResponse<UserDTO, object> userDTO = await _userService.GetUserById(id);
            if (userDTO != null)
            {
                Teacher? teacher = await _teacherRepo.GetByID(userDTO.Payload.TeacherId);
                if(teacher == null)
                {
                    return new ServiceResponse<List<SubjectDTO>, object>(false, "", errors: new object[] { "Teacher not found" });
                }
                IEnumerable<TeacherSubject> TeachersSubjects = await _teachersubjectRepo.GetListBySpec(new TeacherSubjectSpecification.GetByTeacherId(teacher.Id));
                IEnumerable<Subject> subjects = await _subjectRepo.GetListBySpec(new SubjectSpecification.GetByManyId(TeachersSubjects.Select(s => s.SubjectId)));
                List<SubjectDTO> mappedSubjects = subjects.Select(u => _mapper.Map<Subject, SubjectDTO>(u)).ToList();
                return new ServiceResponse<List<SubjectDTO>, object>(true, "", payload: mappedSubjects);
            }
            return new ServiceResponse<List<SubjectDTO>, object>(false, "", errors: new object[] { "Something went wrong" });
        }

        public async Task Update(EditSubjectDTO model)
        {
            await _subjectRepo.Update(_mapper.Map<Subject>(model));
            await _subjectRepo.Save();
        }
    }
}
