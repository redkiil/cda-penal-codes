#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class StatusController : ControllerBase
    {
        private readonly Context _context;

        public StatusController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Rota de Listagem de todos Status criados.
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> Getstatus()
        {
            return await _context.status.ToListAsync();
        }

        /// <summary>
        /// Rota de Listagem de um Status especifico.
        /// </summary>
        /// <param name="id">ID do Status que deseja listar.</param>
        /// <response code="404">Não encontrou o status com este ID!</response>
        /// <remarks>
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            var status = await _context.status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        /// <summary>
        /// Rota de Alteração de um Status especifico.
        /// </summary>
        /// <param name="id">ID do Status que deseja alterar.</param>
        /// <response code="400">O ID inserido query param, não é o mesmo enviado no corpo do JSON.</response>
        /// <response code="404">O Status não existe!</response>
        /// 
        /// <remarks>
        /// Enviar os dados no JSON.
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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
        /// Rota de criação de um Status.
        /// </summary>
        /// <response code="201">O Status foi criado com sucesso!</response>
        /// <remarks>
        /// Enviar os dados no JSON body. 
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            _context.status.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }

        /// <summary>
        /// Rota para deletar um Status.
        /// </summary>
        /// <response code="404">O Status não foi encontrado!</response>
        /// <response code="404">O Status foi deletado com sucesso!</response>
        /// 
        /// <remarks>
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.status.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(int id)
        {
            return _context.status.Any(e => e.Id == id);
        }
    }
}
