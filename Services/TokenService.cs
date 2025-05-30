using System.Security.Cryptography;

namespace InvestigationSupportSystem.Services
{
    public class TokenService
    {
        public string GenerateToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
