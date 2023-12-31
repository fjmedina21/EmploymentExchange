﻿using AutoMapper;
using API.Models;

namespace API
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<LoginDTO, User>();

            CreateMap<UserDTO, User>();
            CreateMap<User, GetUserDTO>();

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, GetRoleDTO>();

            CreateMap<JobTypeDTO, JobType>();
            CreateMap<JobType, GetJobTypeDTO>();

            CreateMap<CompanyDTO, Company>();
            CreateMap<Company, GetCompanyDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, GetCategoryDTO>();

            CreateMap<JobPositionDTO, JobPosition>();
            CreateMap<JobPosition, GetJobPositionDTO>()
                .ForMember(e => e.Category, e => e.MapFrom(x => x.Category.Name));

            CreateMap<JobDTO, Job>();
            CreateMap<Job, GetJobDTO>()
                .ForMember(e => e.Company, e => e.MapFrom(x => x.Company.Name))
                .ForMember(e => e.RecruiterEmail, e => e.MapFrom(x => x.Company.RecruiterEmail))
                .ForMember(e => e.Logo, e => e.MapFrom(x => x.Company.Logo))
                .ForMember(e => e.URL, e => e.MapFrom(x => x.Company.URL))
                .ForMember(e => e.Location, e => e.MapFrom(x => x.Company.Location))
                .ForMember(e => e.Type, e => e.MapFrom(x => x.JobType.Name))
                .ForMember(e => e.Category, e => e.MapFrom(x => x.JobPosition.Category.Name))
                .ForMember(e => e.Position, e => e.MapFrom(x => x.JobPosition.Name))
                .ForMember(e => e.PublishedOn, e => e.MapFrom(x => x.CreatedAt));
        }

    }
}
