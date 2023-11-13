using AutoMapper;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Group> _groupRepo;
        public GroupService(IMapper mapper, IRepository<Group> groupRepo)
        {
            this._mapper = mapper;
            this._groupRepo = groupRepo;
        }
        public async Task Create(CreateGroupDTO model)
        {
            await _groupRepo.Insert(_mapper.Map<Group>(model));
            await _groupRepo.Save();
        }

        public async Task Delete(int id)
        {
            GroupDTO? model = await Get(id);
            if (model == null) return;
            await _groupRepo.Delete(id);
            await _groupRepo.Save();
        }

        public async Task<GroupDTO?> Get(int id)
        {
            if (id < 0) return null;
            Group? category = await _groupRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<GroupDTO?>(category);
        }

        public async Task<List<GroupDTO>> GetAll()
        {
            var result = await _groupRepo.GetAll();
            return _mapper.Map<List<GroupDTO>>(result);
        }

        public async Task<ServiceResponse> GetByName(string name)
        {
            var result = await _groupRepo.GetItemBySpec(new GroupSpecification.GetByName(name));
            if (result != null)
            {
                return new ServiceResponse(false, "Category exists.");
            }
            var category = _mapper.Map<GroupDTO>(result);
            return new ServiceResponse(true, "Category successfully loaded.", payload: category);
        }

        public async Task Update(EditGroupDTO model)
        {
            await _groupRepo.Update(_mapper.Map<Group>(model));
            await _groupRepo.Save();
        }
    }
}
