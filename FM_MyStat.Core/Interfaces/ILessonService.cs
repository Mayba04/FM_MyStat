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
    internal interface ILessonService
    {
        Task<List<LessonsDTO>> GetAll();
        Task<LessonsDTO?> Get(int id);
        Task<ServiceResponse> GetByName(LessonsDTO model);
        Task<LessonsDTO> GetByName(string Name);
        Task Create(CreateLessonsDTO model);
        Task Update(EditLessonsDTO model);
        Task Delete(int id);
    }
}
