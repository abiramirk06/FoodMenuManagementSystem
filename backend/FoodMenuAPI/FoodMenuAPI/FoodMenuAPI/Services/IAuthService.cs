
using FoodMenuAPI.DTOs;

namespace FoodMenuAPI.Services;

public interface IAuthService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO);

    Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
}
