namespace CDA.Services
{
    public static class HashService
    {
        public static string HashPassword(string password)
        {
            var pwd = BCrypt.Net.BCrypt.HashPassword(password);
            return pwd;
        }
        public static bool CheckPassowrd(string formpassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(formpassword, password);
        }
    }
}
