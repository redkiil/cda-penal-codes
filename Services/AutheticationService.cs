using CDA.Data;
using CDA.Models;

namespace CDA.Services
{
    public interface IAuthenticationService
    {
        string? Authenticate(User user);
    }
    public class AutheticationService : IAuthenticationService
    {
        private readonly Context _context;
        public AutheticationService(Context context){
            _context = context;
        }
        public string? Authenticate(User user)
        {
            var userz = _context.users.FirstOrDefault(x => x.UserName == user.UserName);
            if (userz == null)
            {
                return null;
            }
            if(HashService.CheckPassowrd(user.Password, userz.Password))
            {
                var token = JwtService.GenerateToken(user);
                return token;
            }
            return null;
        }
        
    }
}
