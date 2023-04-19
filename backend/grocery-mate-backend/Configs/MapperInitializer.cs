using AutoMapper;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;

namespace grocery_mate_backend.Configs;

public class MapperInitializer : Profile
{
    protected MapperInitializer()
    {
        CreateMap<User, CreateUserDto>().ReverseMap();
        // CreateMap<Address, AddressDto>().ReverseMap();
    }
}