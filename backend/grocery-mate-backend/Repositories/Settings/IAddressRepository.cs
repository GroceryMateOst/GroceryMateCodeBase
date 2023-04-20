using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Data.DataModels.UserManagement.Address;
using grocery_mate_backend.Models;

namespace grocery_mate_backend.Controllers.Repo.Settings;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<Address?> FindAddressByGuid(Guid? guid);
    Task<Address?> FindOrCreateUserAddress(AddressDto addressDto);

    Task<bool?> RemoveAddress(Address address, User user);
}