using System.Net.Sockets;
using AutoMapper;
using grocery_mate_backend.Models;

namespace grocery_mate_backend.Configs;

public class MapperInitializer : Profile
{
    protected MapperInitializer()
    {
        CreateMap<User, CreateUserUserDto>().ReverseMap();
        // CreateMap<Address, AddressDto>().ReverseMap();
    }
}