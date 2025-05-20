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

            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserRequest>();
            CreateMap<CreateUserResponse, User>();
            CreateMap<User, CreateUserResponse>();

            CreateMap<GetUserRequest, User>();
            CreateMap<User, GetUserRequest>();
            CreateMap<GetUserResponse, User>();
            CreateMap<User, GetUserResponse>();

            CreateMap<GetUserRequest, GetUserResponse>();
            CreateMap<GetUserResponse, GetUserRequest>();
        }
    }
}
