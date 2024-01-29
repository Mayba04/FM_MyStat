using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    public interface IHomeworkDoneService
    {
        Task<List<HomeworkDoneDTO>> GetAll();
        Task<List<HomeworkDoneDTO>> GetAll(int homeworkId);
        Task<List<HomeworkDoneDTO>> GetAllByUserId(string studentId);
        Task<HomeworkDoneDTO?> Get(int id);
        Task<ServiceResponse> GetByName(HomeworkDoneDTO model);
        Task<HomeworkDoneDTO> GetByName(string NameDescription);
        Task Create(HomeworkDoneDTO model);
        Task Update(HomeworkDoneDTO model);
        Task Delete(int id);
        Task<(byte[] fileContents, string contentType, string fileName)> DownloadHomeworkFileAsync(int homeworkId);
    }
}
