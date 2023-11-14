using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    internal interface IHomeworkDoneService
    {
        Task<List<HomeworkDoneDTO>> GetAll();

        Task<HomeworkDoneDTO?> Get(int id);
        Task<ServiceResponse> GetByName(HomeworkDoneDTO model);
        Task<HomeworkDoneDTO> GetByName(string NameDescription);
        Task Create(HomeworkDoneDTO model);
        Task Update(HomeworkDoneDTO model);
        Task Delete(int id);
    }
}
