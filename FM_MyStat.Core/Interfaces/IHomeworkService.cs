using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    internal interface IHomeworkService
    {
        Task<List<HomeworkDTO>> GetAll();

        Task<HomeworkDTO?> Get(int id);
        Task<ServiceResponse> GetByName(HomeworkDTO model);
        Task<HomeworkDTO> GetByName(string NameHomework);
        Task Create(HomeworkDTO model);
        Task Update(HomeworkDTO model);
        Task Delete(int id);
    }
}
