﻿using System;
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
    public class SolicitudesController : ControllerBase
    {
        private readonly GranjaCoralesContext _context;

        public SolicitudesController(GranjaCoralesContext context)
        {
            _context = context;
        }

        // GET: api/Solicitudes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitude>>> GetSolicitudes()
        {
          if (_context.Solicitudes == null)
          {
              return NotFound();
          }
            return await _context.Solicitudes.ToListAsync();
        }

        // GET: api/Solicitudes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitude>> GetSolicitude(int id)
        {
          if (_context.Solicitudes == null)
          {
              return NotFound();
          }
            var solicitude = await _context.Solicitudes.FindAsync(id);

            if (solicitude == null)
            {
                return NotFound();
            }

            return solicitude;
        }

        // PUT: api/Solicitudes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitude(int id, Solicitude solicitude)
        {
            if (id != solicitude.IdSolicitud)
            {
                return BadRequest();
            }

            _context.Entry(solicitude).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudeExists(id))
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

        // POST: api/Solicitudes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitude>> PostSolicitude(Solicitude solicitude)
        {
          if (_context.Solicitudes == null)
          {
              return Problem("Entity set 'GranjaCoralesContext.Solicitudes'  is null.");
          }
            _context.Solicitudes.Add(solicitude);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitude", new { id = solicitude.IdSolicitud }, solicitude);
        }

        // DELETE: api/Solicitudes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitude(int id)
        {
            if (_context.Solicitudes == null)
            {
                return NotFound();
            }
            var solicitude = await _context.Solicitudes.FindAsync(id);
            if (solicitude == null)
            {
                return NotFound();
            }

            _context.Solicitudes.Remove(solicitude);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SolicitudeExists(int id)
        {
            return (_context.Solicitudes?.Any(e => e.IdSolicitud == id)).GetValueOrDefault();
        }
    }
}
