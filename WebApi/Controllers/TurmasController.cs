using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TurmasController : ControllerBase
    {
        private readonly WebApiContext _context;

        public TurmasController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/Turmas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas()
        {
            return await _context.Turmas.Include(x => x.Alunos).ToListAsync();
        }

        // GET: api/Turmas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetTurma(int id)
        {
            var turma = await _context.Turmas.Include(x => x.Alunos).FirstOrDefaultAsync(x => x.Id == id);

            if (turma == null)
            {
                return NotFound();
            }

            return turma;
        }

        // PUT: api/Turmas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurma(int id, Turma turma)
        {
            if (id != turma.Id)
            {
                return BadRequest();
            }

            _context.Entry(turma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurmaExists(id))
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

        // POST: api/Turmas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Turma>> PostTurma(Turma turma)
        {

            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurma", new { id = turma.Id }, turma);
 
        }

        // DELETE: api/Turmas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Turma>> DeleteTurma(int id)
        {
            var turma = await _context.Turmas.Include(x => x.Alunos).FirstOrDefaultAsync(x => x.Id == id);
            if (turma == null)
            {
                return NotFound();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Turmas.Remove(turma);

                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {

                    transaction.Rollback();
                }
            }
            


            return NoContent();
        }
        [HttpGet("{id}/alunos")]
        public async Task<ActionResult> GetAlunos(int id)
        {
            var pessoa = await _context.Turmas.Include(x => x.Alunos).FirstOrDefaultAsync(x => x.Id == id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }
        private bool TurmaExists(int id)
        {
            return _context.Turmas.Any(e => e.Id == id);
        }
    }
}
