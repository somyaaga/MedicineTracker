using MedicineTracker.API.Models;
using System.Security.Cryptography;
using System.Text;
using MedicineTracker.API.Interface;
using MedicineTracker.API.DTO;
using MedicineTracker.API.Data;

namespace MedicineTracker.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly MedDBContext _context;
        public AuthService(MedDBContext medDBContext)
        {
            _context = medDBContext;

        }

        //hasing process
        private IQueryable<User> Users => _context.Users;
        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512(); //algo
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }

        
        public async Task<string> RegisterUserAsync(RegisterDTO regDTO)
        {

            var usertab = _context.Users;

            // Prevent duplicate email
            if (usertab.Any(x => x.Email == regDTO.Email))
                return "Email already registered.";

            CreatePasswordHash(regDTO.Password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                Email = regDTO.Email,
                UserName = regDTO.Username,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully.";
        }

        public async Task<User?> LoginAsync(SignInDTO signInDTO)
        {
            //var login; 
            return new User();
        }
    }
}
