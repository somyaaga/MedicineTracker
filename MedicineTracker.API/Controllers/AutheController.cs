using MedicineTracker.API.DTO;
using MedicineTracker.API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutheController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AutheController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Rregister")]
        public async Task<IActionResult> Register(RegisterDTO regDTO)
        {
            var result = await _authService.RegisterUserAsync(regDTO);
            if (result == "User registered successfully.")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

    }
}
