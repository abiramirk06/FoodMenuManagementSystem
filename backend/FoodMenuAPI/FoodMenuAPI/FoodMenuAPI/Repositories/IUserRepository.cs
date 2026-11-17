
using FoodMenuAPI.Auth;

namespace FoodMenuAPI.Repositories;

public interface IUserRepository
{
    Task<AppUser?> GetByEmailAsync(string email);

    Task<AppUser> AddAsync(AppUser user);
}
