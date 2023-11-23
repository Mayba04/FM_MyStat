﻿using AutoMapper;
using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using FM_MyStat.Core.Services.Users;
using Org.BouncyCastle.Ocsp;
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
        private readonly UserService _userService;
        private readonly IRepository<Teacher> _teacherRepo;
        public GroupService(IMapper mapper, IRepository<Group> groupRepo, UserService userService, IRepository<Teacher> teacherRepo)
        {
            this._mapper = mapper;
            this._groupRepo = groupRepo;
            this._userService = userService;
            this._teacherRepo = teacherRepo;
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
                return new ServiceResponse(false, "Group exists.");
            }
            var category = _mapper.Map<GroupDTO>(result);
            return new ServiceResponse(true, "Group successfully loaded.", payload: category);
        }

        public async Task Update(EditGroupDTO model)
        {
            await _groupRepo.Update(_mapper.Map<Group>(model));
            await _groupRepo.Save();
        }

        public async Task<ServiceResponse<EditGroupDTO, object>> GetEditGroupDTO(int id)
        {
            Group? group = await _groupRepo.GetByID(id);
            if (group != null)
            {
                return new ServiceResponse<EditGroupDTO, object>(true, "", payload: _mapper.Map<Group, EditGroupDTO>(group));
            }
            return new ServiceResponse<EditGroupDTO, object>(false, "", errors: new string[] { "Group not found!"});
        }

        public async Task<ServiceResponse<List<GroupDTO>, object>> GetGroupDTOByTeacher(string id)
        {
            ServiceResponse<UserDTO, object> userDTO = await _userService.GetUserById(id);
            if (userDTO.Success)
            {
                Teacher? teacher = await _teacherRepo.GetByID(userDTO.Payload.TeacherId);
                if (teacher != null)
                {
                    if(teacher.Groups != null)
                    {
                        List<GroupDTO> mappedGroups = teacher.Groups.Select(u => _mapper.Map<Group, GroupDTO>(u)).ToList();
                        return new ServiceResponse<List<GroupDTO>, object>(true, "", payload: mappedGroups);
                    }
                    return new ServiceResponse<List<GroupDTO>, object>(false, "", new List<GroupDTO>(), new object[] { "the teacher has no groups" });
                }
            }
            return new ServiceResponse<List<GroupDTO>, object>(false, "", errors: new object[] { "Something went wrong" });
        }
    }
}
