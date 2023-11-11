using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using data = Solution.DO.Objects;
using datamodels = Solution.API.DataModels;
using AutoMapper;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public SolicitudesController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Solicitudes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Solicitudes>>> GetSolicitudes()
        {
            var aux = new BS.Solicitudes(_context).GetAll();
            var mapAux = _mapper.Map<IEnumerable<data.Solicitudes>, IEnumerable<datamodels.Solicitudes>>(aux).ToList();
            return mapAux;
        }

        // GET: api/Solicitudes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Solicitudes>> GetSolicitude(int id)
        {
            var aux = new BS.Solicitudes(_context).GetOneById(id);
            if (aux == null)
            {
                return NotFound();
            }
            var mapAux = _mapper.Map<data.Solicitudes, datamodels.Solicitudes>(aux);
            return mapAux;
        }

        // PUT: api/Solicitudes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitude(int id, datamodels.Solicitudes solicitudes)
        {
            if (id != solicitudes.IdSolicitud)
            {
                return BadRequest();
            }

           

            try
            {
                var mapAux = _mapper.Map<datamodels.Solicitudes, data.Solicitudes>(solicitudes);
                new BS.Solicitudes(_context).Update(mapAux);
            }
            catch (Exception ex)
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
        public async Task<ActionResult<datamodels.Solicitudes>> PostSolicitudes(datamodels.Solicitudes solicitudes)
        {
            var mapAux = _mapper.Map<datamodels.Solicitudes, data.Solicitudes>(solicitudes);
            new BS.Solicitudes(_context).Insert(mapAux);

            return CreatedAtAction("GetSolicitudes", new { id = solicitudes.IdSolicitud }, solicitudes);
        }

        // DELETE: api/Solicitudes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.Solicitudes>> DeleteSolicitude(int id)
        {
            
            var solicitudes = new BS.Solicitudes(_context).GetOneById(id);

            if (solicitudes == null)
            {
                return NotFound();
            }

            new BS.Solicitudes(_context).Delete(solicitudes);
            var mapAux = _mapper.Map<data.Solicitudes, datamodels.Solicitudes>(solicitudes);
            
            return mapAux;
        }

        private bool SolicitudeExists(int id)
        {
            return (new BS.Solicitudes(_context).GetOneById(id)!=null);
        }
    }
}
