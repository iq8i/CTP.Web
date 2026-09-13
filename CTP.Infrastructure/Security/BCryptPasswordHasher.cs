using CTP.Application.Interfaces.Services;

namespace CTP.Infrastructure.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        // Work factor 12 = توازن ممتاز بين الأمان والسرعة
        private const int WorkFactor = 12;

        public string Hash(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentException("كلمة المرور مطلوبة", nameof(plainPassword));

            return BCrypt.Net.BCrypt.HashPassword(plainPassword, WorkFactor);
        }

        public bool Verify(string plainPassword, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(plainPassword) || string.IsNullOrWhiteSpace(storedHash))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.Verify(plainPassword, storedHash);
            }
            catch
            {
                return false;
            }
        }
    }
}