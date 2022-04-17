using Microsoft.AspNetCore.Mvc;
using CDA.Data;
using CDA.Models;
using CDA.Services;

namespace CDA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Context _context;
        private readonly IAuthenticationService _authservice;

        public AuthController(Context context,IAuthenticationService service)
        {
            _context = context;
            _authservice = service;
        }
        /// <summary>
        /// Rota de Login.
        /// </summary>
        /// <response code="200">Logado com sucesso!</response>
        /// <response code="401">Usuario/Senha invalidos!</response>
        /// <remarks>
        /// 
        /// Esta é a rota que o usuario irá logar-se e receber seu Token.
        /// 
        /// No caso de uso no Swagger inserir o token retornado, clicando em "Authorize".
        /// 
        /// Assim liberando todas as outras rotas.
        /// 
        /// 
        /// </remarks>
        [HttpPost]
        [Route("login")]
        public IActionResult Authenticate([FromBody] User user)
        {
            var token = _authservice.Authenticate(user);
            if (token != null)
            {
                return Ok(token);
            }
            return Unauthorized();
        }

    }
}
