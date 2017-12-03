using AutoMapper;
using EmployeeDB.Client.DTOs;
using EmployeeDB.Models;
using EmployeeDB.Services.DTOs;

namespace EmployeeDB.Client
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<PesonalInfoDto, Employee>();
            CreateMap<EmployeeManagerDto, Employee>();
            CreateMap<ManagerDto, Employee>();
        }
    }
}
