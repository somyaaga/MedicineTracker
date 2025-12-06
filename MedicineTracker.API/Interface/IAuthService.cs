using MedicineTracker.API.DTO;
using MedicineTracker.API.Models;

namespace MedicineTracker.API.Interface
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterDTO registerDTO);
        Task<User?> LoginAsync(SignInDTO signInDTO);
    }
}
