namespace CTP.Web
{
    public static class PasswordGenerator
    {
        public static void Main(string[] args)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword("123456", 12);
            Console.WriteLine("============================================");
            Console.WriteLine("BCrypt Hash for '123456':");
            Console.WriteLine(hash);
            Console.WriteLine("============================================");
        }
    }
}