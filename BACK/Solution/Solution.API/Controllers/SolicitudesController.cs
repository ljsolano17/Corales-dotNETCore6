using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using data = Solution.DO.Objects;
using datamodels = Solution.API.DataModels;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public SolicitudesController(SolutionDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _cache = memoryCache;
        }

        // GET: api/Solicitudes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Solicitudes>>> GetSolicitudes()
        {
            var cacheKey = "solicitudesCacheKey";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<datamodels.Solicitudes> cachedCategorias))
            {
                return cachedCategorias.ToList();
            }
            var aux = new BS.Solicitudes(_context).GetAll();
            var mapAux = _mapper.Map<IEnumerable<data.Solicitudes>, IEnumerable<datamodels.Solicitudes>>(aux).ToList();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, mapAux, cacheOptions);

            return mapAux;
        }

        // GET: api/Solicitudes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Solicitudes>> GetSolicitude(int id)
        {
            var cacheKey = "solicitudesCacheKey";
            if (_cache.TryGetValue(cacheKey, out datamodels.Solicitudes cachedCategorias))
            {
                return cachedCategorias;
            }
            var aux = new BS.Solicitudes(_context).GetOneById(id);
            if (aux == null)
            {
                return NotFound();
            }
            var mapAux = _mapper.Map<data.Solicitudes, datamodels.Solicitudes>(aux);

            var cacheOptions = new MemoryCacheEntryOptions()
              .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, mapAux, cacheOptions);

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
                var cacheKey = "solicitudesCacheKey";
                _cache.Remove(cacheKey);
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
            var cacheKey = "solicitudesCacheKey";
            _cache.Remove(cacheKey);
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

            var cacheKey = "solicitudesCacheKey";
            _cache.Remove(cacheKey);

            return mapAux;
        }

        private bool SolicitudeExists(int id)
        {
            return (new BS.Solicitudes(_context).GetOneById(id)!=null);
        }
    }
}
