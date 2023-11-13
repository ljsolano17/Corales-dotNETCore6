using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using datamodels = Solution.API.DataModels;
using data = Solution.DO.Objects;
using Microsoft.Extensions.Caching.Memory;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ArticulosController(SolutionDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _cache = memoryCache;
        }

        // GET: api/Articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Articulos>>> GetArticulos()
        {
            var cacheKey = "articulosCacheKey";
            if(_cache.TryGetValue(cacheKey, out IEnumerable<datamodels.Articulos> cachedArticulos))
            {
                return cachedArticulos.ToList();
            }

            var aux = await new BS.Articulos(_context).GetAllWithAsync();
            var mapAux = _mapper.Map<IEnumerable<data.Articulos>,IEnumerable<datamodels.Articulos>>(aux).ToList();
            
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, mapAux, cacheOptions);

            return mapAux;
        }

        // GET: api/Articulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Articulos>> GetArticulo(int id)
        {
            var cacheKey = "articulosCacheKey";
            if (_cache.TryGetValue(cacheKey, out datamodels.Articulos cachedArticulos))
            {
                return cachedArticulos;
            }
            var aux = await new BS.Articulos(_context).GetOneByIdWithAsync(id);
            var mapAux = _mapper.Map<data.Articulos, datamodels.Articulos>(aux);

            if (mapAux == null)
            {
                return NotFound();
            }
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, mapAux, cacheOptions);

            return mapAux;
        }

        // PUT: api/Articulos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulo(int id, datamodels.Articulos articulos)
        {
            if (id != articulos.IdArticulo)
            {
                return BadRequest();
            }

            try
            {
                var mapAux = _mapper.Map<datamodels.Articulos, data.Articulos>(articulos);
                new BS.Articulos(_context).Update(mapAux);
                var cacheKey = "articulosCacheKey";
                _cache.Remove(cacheKey);
            }
            catch (Exception ex)
            {
                if (!ArticuloExists(id))
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

        // POST: api/Articulos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<datamodels.Articulos>> PostArticulo(datamodels.Articulos articulos)
        {
            var mapAux = _mapper.Map<datamodels.Articulos, data.Articulos>(articulos);
            new BS.Articulos(_context).Insert(mapAux);

            var cacheKey = "articulosCacheKey";
            _cache.Remove(cacheKey);

            return CreatedAtAction("GetArticulos", new { id = articulos.IdArticulo }, articulos);
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.Articulos>> DeleteArticulo(int id)
        {
            var articulos = new BS.Articulos(_context).GetOneById(id);
            if (articulos == null)
            {
                return NotFound();
            }

            new BS.Articulos(_context).Delete(articulos);
            var mapAux = _mapper.Map<data.Articulos,datamodels.Articulos>(articulos);

            var cacheKey = "articulosCacheKey";
            _cache.Remove(cacheKey);

            return mapAux;
        }

        private bool ArticuloExists(int id)
        {
            return (new BS.Articulos(_context).GetOneById!=null);
        }
    }
}
