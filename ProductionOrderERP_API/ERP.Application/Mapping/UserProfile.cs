using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<GetUserTypeRequest, UserType>();
            CreateMap<UserType, GetUserTypeRequest>();
            CreateMap<GetUserTypeResponse, UserType>();
            CreateMap<UserType, GetUserTypeResponse>();

            CreateMap<CreateUserRequest, UserType>();
            CreateMap<UserType, CreateUserRequest>();
            CreateMap<CreateUserResponse, UserType>();
            CreateMap<UserType, CreateUserResponse>();

            CreateMap<GetUserRequest, UserType>();
            CreateMap<UserType, GetUserRequest>();
            CreateMap<GetUserResponse, UserType>();
            CreateMap<UserType, GetUserResponse>();
        }
    }
}
