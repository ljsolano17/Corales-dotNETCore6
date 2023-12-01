using AutoMapper;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using data = Solution.DO.Objects;
using datamodels = Solution.API.DataModels;
using Microsoft.Extensions.Caching.Memory;
using Solution.DAL.Repository;
using Solution.API.DataModels;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CategoriasController(SolutionDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _cache = memoryCache;
        }

      

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Categorias>>> GetCategorias()
        {
            var cacheKey = "categoriasCacheKey";
            if(_cache.TryGetValue(cacheKey, out IEnumerable<datamodels.Categorias> cachedCategorias)) 
            {
                return cachedCategorias.ToList();
            }
            var aux = new BS.Categorias(_context).GetAll();
            var mapAux = _mapper.Map<IEnumerable<data.Categorias>, IEnumerable<datamodels.Categorias>>(aux).ToList();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey,mapAux, cacheOptions);
            
            return mapAux;
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Categorias>> GetCategoria(int id)
        {
            var cacheKey = "categoriasCacheKey";
            if (_cache.TryGetValue(cacheKey, out datamodels.Categorias cachedCategorias))
            {
                return cachedCategorias;
            }

            var aux = new BS.Categorias(_context).GetOneById(id);
            if (aux == null)
            {
                return NotFound();
            }
            var mapAux = _mapper.Map<data.Categorias, datamodels.Categorias>(aux);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, mapAux, cacheOptions);

            return mapAux;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, datamodels.Categorias categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest();
            }

            
            try
            {
                var mapAux = _mapper.Map<datamodels.Categorias, data.Categorias>(categoria);
                new BS.Categorias(_context).Update(mapAux);

                var cacheKey = "categoriasCacheKey";
                _cache.Remove(cacheKey);
            }
            catch (Exception ex)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<datamodels.Categorias>> PostCategoria(datamodels.Categorias categoria)
        {
            var mapAux = _mapper.Map<datamodels.Categorias, data.Categorias>(categoria);
            new BS.Categorias(_context).Insert(mapAux);

            var cacheKey = "categoriasCacheKey";
            _cache.Remove(cacheKey);

            return CreatedAtAction("GetCategoria", new { id = categoria.IdCategoria }, categoria);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.Categorias>> DeleteCategoria(int id)
        {
           
            var categoria = new BS.Categorias(_context).GetOneById(id);

            if (categoria == null)
            {
                return NotFound();
            }

            new BS.Categorias(_context).Delete(categoria);
            var mapAux = _mapper.Map<data.Categorias, datamodels.Categorias>(categoria);

            var cacheKey = "categoriasCacheKey";
            _cache.Remove(cacheKey);

            return mapAux;
        }

        private bool CategoriaExists(int id)
        {
            return (new BS.Categorias(_context).GetOneById(id)!=null);
        }
    }
}
