using AutoMapper;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.SERVICE.DTOs.GymClass;
using GymMembershipManagement.SERVICE.DTOs.Membership;
using GymMembershipManagement.SERVICE.DTOs.Role;
using GymMembershipManagement.SERVICE.DTOs.Schedule;
using GymMembershipManagement.SERVICE.DTOs.User;

namespace GymMembershipManagement.SERVICE.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // GymClass
            CreateMap<GymClass, GymClassDto>();
            CreateMap<CreateGymClassDto, GymClass>();
            CreateMap<UpdateGymClassDto, GymClass>();

            // Role
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

            // User
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person != null ? src.Person.FirstName : null))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person != null ? src.Person.LastName : null));
            CreateMap<UserRegisterModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UpdateUserModel, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Membership
            CreateMap<Membership, MembershipDTO>()
                .ForMember(dest => dest.MembershipTypeName, opt => opt.MapFrom(src => src.MembershipType != null ? src.MembershipType.MembershipTypeName : ""))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
            CreateMap<RegisterMembershipDTO, Membership>();

            // Schedule
            CreateMap<Schedule, ScheduleDTO>()
                .ForMember(dest => dest.GymClassName, opt => opt.MapFrom(src => src.GymClass != null ? src.GymClass.GymClassName : null));
            CreateMap<AssignScheduleDTO, Schedule>();
        }
    }
}
