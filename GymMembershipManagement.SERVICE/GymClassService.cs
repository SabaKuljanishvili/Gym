using AutoMapper;
using GymMembershipManagement.DAL.Repositories;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.SERVICE.DTOs.GymClass;
using GymMembershipManagement.SERVICE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.SERVICE
{
    public class GymClassService : IGymClassService
    {

        private readonly IGymClassRepository _repository;
        private readonly IMapper _mapper;

        public GymClassService(IGymClassRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GymClassDto>> GetAllGymClassesAsync()
        {
            var gymClasses = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GymClassDto>>(gymClasses);
        }

        public async Task<GymClassDto> GetGymClassByIdAsync(int id)
        {
            var gymClass = await _repository.GetByIdAsync(id);
            return _mapper.Map<GymClassDto>(gymClass);
        }

        public async Task<GymClassDto> CreateGymClassAsync(CreateGymClassDto model)
        {
            var gymClass = _mapper.Map<GymClass>(model);
            await _repository.AddAsync(gymClass);

            return _mapper.Map<GymClassDto>(gymClass);
        }

        public async Task<bool> DeleteGymClassAsync(int id)
        {
            var gymClass = await _repository.GetByIdAsync(id);

            if (gymClass == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }


        public async Task<bool> UpdateGymClassAsync(int id, UpdateGymClassDto model)
        {
            var gymClass = await _repository.GetByIdAsync(id);

            if (gymClass == null) return false;

            _mapper.Map(model, gymClass);

            await _repository.UpdateAsync(gymClass);
            return true;
        }
    }

}

