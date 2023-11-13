using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using datamodels = Solution.API.DataModels;
using data = Solution.DO.Objects;
using Solution.DAL.EF;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosSolicitudController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ArticulosSolicitudController(SolutionDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _cache = memoryCache;
        }

        // GET: api/ArticulosSolicitud
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.ArticulosSolicitud>>> GetArticulosSolicitud()
        {
            var cacheKey = "ArticulosSolicitudCacheKey";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<datamodels.ArticulosSolicitud> cachedArticulosSolicitud))
            {
                return cachedArticulosSolicitud.ToList();
            }
            var aux = await new BS.ArticulosSolicitud(_context).GetAllWithAsync();
            var mapAux = _mapper.Map<IEnumerable<data.ArticulosSolicitud>,IEnumerable<datamodels.ArticulosSolicitud>>(aux).ToList();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, mapAux, cacheOptions);

            return mapAux;
            
        }

        // GET: api/ArticulosSolicitud/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.ArticulosSolicitud>> GetArticulosSolicitud(int id)
        {
            var cacheKey = "ArticulosSolicitudCacheKey";
            if (_cache.TryGetValue(cacheKey, out datamodels.ArticulosSolicitud cachedArticulosSolicitud))
            {
                return cachedArticulosSolicitud;
            }

            var aux = await new BS.ArticulosSolicitud(_context).GetOneByIdWithAsync(id);
            var mapAux = _mapper.Map<data.ArticulosSolicitud, datamodels.ArticulosSolicitud>(aux);


            if (mapAux == null)
            {
                return NotFound();
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, mapAux, cacheOptions);

            return mapAux;
        }

        // PUT: api/ArticulosSolicitud/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulosSolicitud(int id, datamodels.ArticulosSolicitud articulosSolicitud)
        {
            if (id != articulosSolicitud.IdArticuloSolicitud)
            {
                return BadRequest();
            }

            

            try
            {
                var mapAux = _mapper.Map<datamodels.ArticulosSolicitud, data.ArticulosSolicitud>(articulosSolicitud);
                new BS.ArticulosSolicitud(_context).Update(mapAux);
                var cacheKey = "ArticulosSolicitudCacheKey";
                _cache.Remove(cacheKey);
            }
            catch (Exception ex)
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
        public async Task<ActionResult<datamodels.ArticulosSolicitud>> PostArticulosSolicitud(datamodels.ArticulosSolicitud articulosSolicitud)
        {

            var mapAux = _mapper.Map<datamodels.ArticulosSolicitud,data.ArticulosSolicitud>(articulosSolicitud);
            new BS.ArticulosSolicitud (_context).Insert(mapAux);
            var cacheKey = "ArticulosSolicitudCacheKey";
            _cache.Remove(cacheKey);

            return CreatedAtAction("GetArticulosSolicitud", new { id = articulosSolicitud.IdArticuloSolicitud }, articulosSolicitud);
        }

        // DELETE: api/ArticulosSolicitud/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.ArticulosSolicitud>> DeleteArticulosSolicitud(int id)
        {
           
            var articulosSolicitud = new BS.ArticulosSolicitud(_context).GetOneById(id);
            
            if (articulosSolicitud == null)
            {
                return NotFound();
            }

            new BS.ArticulosSolicitud(_context).Delete(articulosSolicitud);

            var mapAux = _mapper.Map<data.ArticulosSolicitud,datamodels.ArticulosSolicitud>(articulosSolicitud);
            var cacheKey = "ArticulosSolicitudCacheKey";
            _cache.Remove(cacheKey);

            return mapAux;
        }

        private bool ArticulosSolicitudExists(int id)
        {
            return (new BS.ArticulosSolicitud(_context).GetOneById(id)!=null);
        }
    }
}
