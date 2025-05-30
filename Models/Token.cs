namespace InvestigationSupportSystem.Models{
    public class Token
    {
        public static string Generate() => Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(32));
    }
}