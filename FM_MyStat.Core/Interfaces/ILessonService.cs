using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    public interface ILessonService
    {
        Task<List<LessonDTO>> GetAll();
        Task<LessonDTO?> Get(int id);
        Task<ServiceResponse> GetByName(LessonDTO model);
        Task<LessonDTO> GetByName(string Name);
        Task Create(CreateLessonsDTO model);
        Task Update(EditLessonsDTO model);
        Task Delete(int id);
        Task<ServiceResponse<List<LessonDTO>, object>> GetLessonDTOByTeacher(string id);
    }
}
