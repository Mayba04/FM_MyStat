using FM_MyStat.Core.DTOs.LessonsDTO.LessonMark;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    internal interface ILessonMarkService
    {
        Task<List<LessonMarkDTO>> GetAll();
        Task<LessonMarkDTO?> Get(int id);
        Task<ServiceResponse> GetById(LessonMarkDTO model);
        Task<LessonMarkDTO> GetById(int Id);
        Task Create(CreateLessonMarkDTO model);
        Task Update(EditLessonMarkDTO model);
        Task Delete(int id);
        Task<List<StudentDTO>> GetAllStudents();
    }
}
