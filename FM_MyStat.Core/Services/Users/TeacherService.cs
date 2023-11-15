using AutoMapper;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services.Users
{
    public class TeacherService: ITeacherService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Teacher> _teacherRepo;

        public TeacherService(IMapper mapper, IRepository<Teacher> homeRepo)
        {
            _teacherRepo = homeRepo;
            _mapper = mapper;
        }

        public async Task Create(TeacherDTO model)
        {
            await _teacherRepo.Insert(_mapper.Map<Teacher>(model));
            await _teacherRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _teacherRepo.Delete(model.Id);
            await _teacherRepo.Save();
        }

        public async Task<TeacherDTO?> Get(int id)
        {
            if (id < 0) return null;
            var category = await _teacherRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<TeacherDTO?>(category);

        }

        public async Task<List<TeacherDTO>> GetAll()
        {
            var result = await _teacherRepo.GetAll();
            return _mapper.Map<List<TeacherDTO>>(result);
        }

        public async Task<ServiceResponse> GetById(TeacherDTO model)
        {
            var result = await _teacherRepo.GetItemBySpec(new TeacherSpecification.GetById(model.Id));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Homework exists."
                };
            }
            var category = _mapper.Map<StudentDTO>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Homework successfully loaded.",
                Payload = category
            };
        }

        public async Task<TeacherDTO> GetById(int Id)
        {
            var result = await _teacherRepo.GetItemBySpec(new TeacherSpecification.GetById(Id));
            if (result != null)
            {
                TeacherDTO categoryDTO = _mapper.Map<TeacherDTO>(result);
                return categoryDTO;
            }
            return null;
        }


        public async Task Update(TeacherDTO model)
        {
            await _teacherRepo.Update(_mapper.Map<Teacher>(model));
            await _teacherRepo.Save();
        }
    }
}
