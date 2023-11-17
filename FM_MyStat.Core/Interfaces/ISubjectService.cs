using FM_MyStat.Core.DTOs.SubjectsDTO;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    public interface ISubjectService
    {
        Task<List<SubjectDTO>> GetAll();
        Task<SubjectDTO?> Get(int id);
        Task<ServiceResponse> GetByName(SubjectDTO model);
        Task<SubjectDTO> GetByName(string NameSubject);
        Task Create(CreateSubjectDTO model);
        Task Update(EditSubjectDTO model);
        Task Delete(int id);

    }
}
