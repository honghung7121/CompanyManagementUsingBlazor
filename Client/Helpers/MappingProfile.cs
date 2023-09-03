using AutoMapper;
using BusinessObjects;
using BusinessObjects.Dto;

namespace Client.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
        }
    }
}
