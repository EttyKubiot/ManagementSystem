using AutoMapper;
using ManagementSystem.Core.DTOs;
using ManagementSystem.Data;
using ManagementSystem.Data.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManagementSystem.Core
{
    public class MappingProfile: Profile
    {
        
    
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<CreateUserDTO, User>();

            CreateMap<WorkHours, WorkHoursDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<LeaveRequest, LeaveRequestDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
        }
    }
}

