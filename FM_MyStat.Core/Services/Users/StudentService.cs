using AutoMapper;
using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.Entities.Homeworks;
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
    public class StudentService: IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Student> _studentRepo;

        public StudentService(IMapper mapper, IRepository<Student> homeRepo)
        {
            _studentRepo = homeRepo;
            _mapper = mapper;
        }

        public async Task Create(StudentDTO model)
        {
            await _studentRepo.Insert(_mapper.Map<Student>(model));
            await _studentRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _studentRepo.Delete(model.Id);
            await _studentRepo.Save();
        }

        public async Task<StudentDTO?> Get(int id)
        {
            if (id < 0) return null;
            var category = await _studentRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<StudentDTO?>(category);

        }

        public async Task<List<StudentDTO>> GetAll()
        {
            var result = await _studentRepo.GetAll();
            return _mapper.Map<List<StudentDTO>>(result);
        }

        public async Task<ServiceResponse> GetById(StudentDTO model)
        {
            var result = await _studentRepo.GetItemBySpec(new StudentSpecification.GetById(model.Id));
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

        public async Task<StudentDTO> GetById(int Id)
        {
            var result = await _studentRepo.GetItemBySpec(new StudentSpecification.GetById(Id));
            if (result != null)
            {
                StudentDTO categoryDTO = _mapper.Map<StudentDTO>(result);
                return categoryDTO;
            }
            return null;
        }


        public async Task Update(StudentDTO model)
        {
            await _studentRepo.Update(_mapper.Map<Student>(model));
            await _studentRepo.Save();
        }
    }
}
