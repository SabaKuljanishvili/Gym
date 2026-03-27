using AutoMapper;
using GymMembershipManagement.DAL.Repositories;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.SERVICE.DTOs.Role;
using GymMembershipManagement.SERVICE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.SERVICE
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetRoleByIdAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> CreateRoleAsync(CreateRoleDto model)
        {
            var role = _mapper.Map<Role>(model);
            await _repository.AddAsync(role);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> UpdateRoleAsync(int id, UpdateRoleDto model)
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null) return false;

            _mapper.Map(model, role);
            await _repository.UpdateAsync(role);
            return true;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
