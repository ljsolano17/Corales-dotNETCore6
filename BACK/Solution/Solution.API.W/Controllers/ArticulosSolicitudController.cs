using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.API.W.Models;

namespace Solution.API.W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosSolicitudController : ControllerBase
    {
        private readonly GranjaCoralesContext _context;

        public ArticulosSolicitudController(GranjaCoralesContext context)
        {
            _context = context;
        }

        // GET: api/ArticulosSolicitud
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticulosSolicitud>>> GetArticulosSolicituds()
        {
          if (_context.ArticulosSolicituds == null)
          {
              return NotFound();
          }
            return await _context.ArticulosSolicituds.ToListAsync();
        }

        // GET: api/ArticulosSolicitud/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticulosSolicitud>> GetArticulosSolicitud(int id)
        {
          if (_context.ArticulosSolicituds == null)
          {
              return NotFound();
          }
            var articulosSolicitud = await _context.ArticulosSolicituds.FindAsync(id);

            if (articulosSolicitud == null)
            {
                return NotFound();
            }

            return articulosSolicitud;
        }

        // PUT: api/ArticulosSolicitud/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulosSolicitud(int id, ArticulosSolicitud articulosSolicitud)
        {
            if (id != articulosSolicitud.IdArticuloSolicitud)
            {
                return BadRequest();
            }

            _context.Entry(articulosSolicitud).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticulosSolicitudExists(id))
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

        // POST: api/ArticulosSolicitud
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticulosSolicitud>> PostArticulosSolicitud(ArticulosSolicitud articulosSolicitud)
        {
          if (_context.ArticulosSolicituds == null)
          {
              return Problem("Entity set 'GranjaCoralesContext.ArticulosSolicituds'  is null.");
          }
            _context.ArticulosSolicituds.Add(articulosSolicitud);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticulosSolicitud", new { id = articulosSolicitud.IdArticuloSolicitud }, articulosSolicitud);
        }

        // DELETE: api/ArticulosSolicitud/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulosSolicitud(int id)
        {
            if (_context.ArticulosSolicituds == null)
            {
                return NotFound();
            }
            var articulosSolicitud = await _context.ArticulosSolicituds.FindAsync(id);
            if (articulosSolicitud == null)
            {
                return NotFound();
            }

            _context.ArticulosSolicituds.Remove(articulosSolicitud);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticulosSolicitudExists(int id)
        {
            return (_context.ArticulosSolicituds?.Any(e => e.IdArticuloSolicitud == id)).GetValueOrDefault();
        }
    }
}
