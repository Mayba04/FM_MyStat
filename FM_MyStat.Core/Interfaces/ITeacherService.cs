using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    internal interface ITeacherService
    {
        Task<List<TeacherDTO>> GetAll();
        Task<TeacherDTO?> Get(int id);
        Task<ServiceResponse> GetById(TeacherDTO model);
        Task<TeacherDTO> GetById(int Id);
        Task Create(TeacherDTO model);
        Task Update(TeacherDTO model);
        Task Delete(int id);
    }
}
