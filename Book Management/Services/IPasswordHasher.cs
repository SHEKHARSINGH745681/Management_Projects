using Book_Management.Models;
using Microsoft.AspNetCore.Identity;

namespace Book_Management.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class PasswordHasher : IPasswordHasher
    {
        private readonly Microsoft.AspNetCore.Identity.PasswordHasher<User> _hasher;

        public PasswordHasher()
        {
            _hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
        }

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            var result = _hasher.VerifyHashedPassword(null, hash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}

