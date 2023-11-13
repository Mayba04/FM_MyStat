using AutoMapper;
using FM_MyStat.Core.DTOs.SubjectsDTO;
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
    public class SubjectService : ISubjectService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Subject> _subjectRepo;

        public SubjectService(IMapper mapper, IRepository<Subject> subjectRepo)
        {
            _subjectRepo = subjectRepo;
            _mapper = mapper;
        }

        public async Task Create(SubjectDTO model)
        {
            await _subjectRepo.Insert(_mapper.Map<Subject>(model));
            await _subjectRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _subjectRepo.Delete(model.Id);
            await _subjectRepo.Save();
        }

        public async Task<SubjectDTO?> Get(int id)
        {
            if (id < 0) return null;
            var subject = await _subjectRepo.GetByID(id);
            if (subject == null) return null;
            return _mapper.Map<SubjectDTO?>(subject);

        }

        public async Task<List<SubjectDTO>> GetAll()
        {
            var result = await _subjectRepo.GetAll();
            return _mapper.Map<List<SubjectDTO>>(result);
        }

        public async Task<ServiceResponse> GetByName(SubjectDTO model)
        {
            var result = await _subjectRepo.GetItemBySpec(new SubjectSpecification.GetByName(model.Name));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Subject exists."
                };
            }
            var subject = _mapper.Map<SubjectDTO>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Subject successfully loaded.",
                Payload = subject
            };
        }

        public async Task<SubjectDTO> GetByName(string NameSubject)
        {
            var result = await _subjectRepo.GetItemBySpec(new SubjectSpecification.GetByName(NameSubject));
            if (result != null)
            {
                SubjectDTO subjectDTO = _mapper.Map<SubjectDTO>(result);
                return subjectDTO;
            }
            return null;
        }

        public async Task Update(SubjectDTO model)
        {
            await _subjectRepo.Update(_mapper.Map<Subject>(model));
            await _subjectRepo.Save();
        }
    }
}
