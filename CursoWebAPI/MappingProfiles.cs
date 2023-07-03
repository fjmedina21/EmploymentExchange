using AutoMapper;
using EmploymentExchange.Models;

namespace EmploymentExchange
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<LoginDTO, User>();
            CreateMap<User, LoggedInDTO>();             

            CreateMap<UserDTO, User>();
            CreateMap<User, READUserDTO>();                

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, READRoleDTO>();       

            CreateMap<JobTypeDTO, JobType>();
            CreateMap<JobType, READJobTypeDTO>();

            CreateMap<CompanyDTO, Company>();
            CreateMap<Company, READCompanyDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, READCategoryDTO>();

            CreateMap<JobPositionDTO, JobPosition>();
            CreateMap<JobPosition, READJobPositionDTO>()
                .ForMember(e => e.Category, e => e.MapFrom(x => x.Category.Name));

            CreateMap<JobDTO, Job>();
            CreateMap<Job, READJobDTO>()
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
