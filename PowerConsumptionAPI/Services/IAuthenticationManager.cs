using PowerConsumptionAPI.Models.DTOs.User;

namespace PowerConsumptionAPI.Services
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
