#nullable disable
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CDA.Data;
using CDA.Models;
using Microsoft.AspNetCore.Authorization;

namespace CDA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CriminalCodesController : ControllerBase
    {
        private readonly Context _context;

        public CriminalCodesController(Context context)
        {
            _context = context;
        }
        /// <summary>
        /// Lista todos Códigos Penais.
        /// </summary>
        /// <param name="filter">Parametro de filtro, irá buscar esta palavra nos campos "Description" e "Name"</param>
        /// <param name="by">Parametro de ordenação, default = Id, pode ser qualquer parametro Ex: CreateDate, UpdateDate etc...</param>
        /// <param name="order">Ordem ASC = crescente (valor default), DESC decrescente.</param>
        /// <param name="page">Paginação, lista 5 valores por pagina.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CriminalCodeDto>>> GetcriminalCodes(string filter = "", string by = "Id", string order = "ASC", int page = 0)
        {
            var filter_handle = _context.criminalCodes.Where(x => x.Description.Contains(filter) || x.Name.Contains(filter) || x.CreateUser.UserName.Contains(filter));
            var relatioship_handle = filter_handle.Include(b => b.CreateUser).Include(b => b.Status).Include(b => b.UpdateUser);
            var pagination_handle = relatioship_handle.OrderBy($"{by} {order}").Skip(page * 5).Take(5);
            var selection = pagination_handle.Select(x => new CriminalCodeDto() {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Penalty = x.Penalty,
                PrisionTime = x.PrisionTime,
                StatusId = x.StatusId,
                CreateUserId = x.CreateUserId,
                UpdateUserId = x.UpdateUserId,
                Status = x.Status,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                CreateUser = x.CreateUser,
                UpdateUser = x.UpdateUser,
            });//usando dto para esconder alguns valores(password do usuario)
            var result = await selection.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Rota de Listagem de um Código Penal Especifico.
        /// </summary>
        /// <response code="200">Retorna um codigo penal!</response>
        /// <response code="404">Não encontrou codigo penal especifico!</response>
        /// <param name="id">ID do Codigo Penal que deseja listar.</param>
        /// <remarks>
        /// 
        /// Listagem por ID, insira o ID do código penal.
        /// 
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<CriminalCode>> GetCriminalCode(int id)
        {
            var criminalCode = await _context.criminalCodes.Include(b => b.CreateUser).Include(b => b.Status).Include(b => b.UpdateUser).SingleOrDefaultAsync(c=>c.Id==id);

            if (criminalCode == null)
            {
                return NotFound();
            }

            return criminalCode;
        }

        /// <summary>
        /// Rota de Atualização de um Código Penal.
        /// </summary>
        /// <response code="400">O ID inserido como query params, não é o mesmo enviado no JSON do body!</response>
        /// <response code="404">O ID inserido não existe no banco de dados para atualizar!</response>
        /// <response code="204">Item alterado com sucesso!</response>
        /// <param name="id">ID do Codigo Penal que deseja atualizar.</param>
        /// <remarks>
        /// 
        /// Este endpoint, utiliza os mesmo parametros do metodo POST.
        /// Caso seja outro usuario que editou, tem que enviar no campo UpdateUserId.
        /// Que deve vir pelo front-end do usuario logado.
        /// 
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCriminalCode(int id, CriminalCodeDto criminalCode)
        {
            if (id != criminalCode.Id)
            {
                return BadRequest();
            }
            criminalCode.UpdateDate = DateTime.Now.ToUniversalTime();
            var exist = _context.criminalCodes.FirstOrDefault(b => b.Id == id);
            _context.Entry(exist).CurrentValues.SetValues(criminalCode);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriminalCodeExists(id))
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
        /// Cria um novo Código Penal.
        /// </summary>
        /// <response code="201">Código Penal criado com sucesso!</response>
        /// <remarks>
        /// 
        /// 
        /// Inserir o ID de Status que deve vir pelo front-end.
        /// 
        /// Inserir o ID do Usuario que criou que deve vir pelo front-end.
        /// 
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<CriminalCode>> PostCriminalCode(CriminalCodeBase criminalCode)
        {
            //TODO: refactor this if i have time
            var item = new CriminalCode()
            {
                Name = criminalCode.Name,
                Description = criminalCode.Description,
                Penalty = criminalCode.Penalty,
                PrisionTime = criminalCode.PrisionTime,
                StatusId = criminalCode.StatusId,
                CreateDate = DateTime.Now.ToUniversalTime(),
                UpdateDate = DateTime.Now.ToUniversalTime(),
                CreateUserId = criminalCode.CreateUserId,
                UpdateUserId = criminalCode.UpdateUserId
            };

            _context.criminalCodes.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCriminalCode", new { id = item.Id }, item);
        }

        /// <summary>
        /// Deleta um Código Penal especifico.
        /// </summary>
        /// <response code="404">Código Penal não foi encontrado!</response>
        /// <response code="204">Código Penal foi deletado com sucesso!</response>
        /// <param name="id">ID do Codigo Penal que deseja deletar.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriminalCode(int id)
        {
            var criminalCode = await _context.criminalCodes.FindAsync(id);
            if (criminalCode == null)
            {
                return NotFound();
            }

            _context.criminalCodes.Remove(criminalCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CriminalCodeExists(int id)
        {
            return _context.criminalCodes.Any(e => e.Id == id);
        }
    }
}
