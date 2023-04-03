using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>()
                .ForMember(setName => setName.name,opt => opt
                .MapFrom(src => $"{src.firstName} {src.middleName} {src.lastName}"));
        }
    }
}
