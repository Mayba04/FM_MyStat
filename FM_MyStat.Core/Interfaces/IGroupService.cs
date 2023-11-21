using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    public interface IGroupService
    {
        Task<List<GroupDTO>> GetAll();
        Task<GroupDTO?> Get(int id);
        Task<ServiceResponse> GetByName(string name);
        Task Create(CreateGroupDTO model);
        Task Update(EditGroupDTO model);
        Task Delete(int id);
        Task<ServiceResponse<EditGroupDTO, object>> GetEditGroupDTO(int id);
    }
}
