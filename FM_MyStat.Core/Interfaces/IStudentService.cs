
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    internal interface IStudentService
    {
        Task<List<StudentDTO>> GetAll();

        Task<StudentDTO?> Get(int id);
        Task<ServiceResponse> GetById(StudentDTO model);
        Task<StudentDTO> GetById(int Id);
        Task Create(StudentDTO model);
        Task Update(StudentDTO model);
        Task Delete(int id);
    }
}
