using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CDA.Data;
using CDA.Models;
using CDA.Services;
using Microsoft.AspNetCore.Authorization;

namespace CDA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Rota de listagem de todos usuarios!
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Getusers(string by = "Id", string order = "ASC", int page = 0)
        {
    
            var user = await _context.users.OrderBy($"{by} {order}").Skip(page * 5).Take(5).Select(u => new UserDto() { Id = u.Id, UserName = u.UserName }).ToListAsync();
            return user;
        }

        /// <summary>
        /// Rota de listagem de um Usuario especifico!
        /// </summary>
        /// <param name="id">ID do usuario que deseja listar!</param>
        /// <response code="404">Não encontrou usuario especifico!</response>
        /// <remarks>
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.users.Select(u => new UserDto() { Id = u.Id, UserName = u.UserName }).SingleOrDefaultAsync(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Rota de Atualização de um Usuario.
        /// </summary>
        /// <response code="400">O ID inserido como query params, não é o mesmo enviado no JSON do body!</response>
        /// <response code="404">O ID inserido não existe no banco de dados para atualizar!</response>
        /// <response code="204">Item alterado com sucesso!</response>
        /// <param name="id">ID do Usuario que deseja atualizar.</param>
        /// <remarks>
        /// 
        /// 
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            user.Password = HashService.HashPassword(user.Password);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Cria um novo Usuario.
        /// </summary>
        /// <response code="201">Usuario criado com sucesso!</response>
        /// <response code="409">Usuario já existe!</response>
        /// <remarks>
        /// 
        /// </remarks>
        /// 
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var exists = _context.users.Any(x => x.UserName == user.UserName);
            if (exists)
            {
                return Conflict("Usuario com este nome já existe!");
            }
            user.Password = HashService.HashPassword(user.Password);
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.Id == id);
        }
    }
}
