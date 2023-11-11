using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using datamodels = Solution.API.DataModels;
using data = Solution.DO.Objects;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public ArticulosController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Articulos>>> GetArticulos()
        {
            var aux = await new BS.Articulos(_context).GetAllWithAsync();
            var mapAux = _mapper.Map<IEnumerable<data.Articulos>,IEnumerable<datamodels.Articulos>>(aux).ToList();
            
            return mapAux;
        }

        // GET: api/Articulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Articulos>> GetArticulo(int id)
        {
           
            var aux = await new BS.Articulos(_context).GetOneByIdWithAsync(id);
            var mapAux = _mapper.Map<data.Articulos, datamodels.Articulos>(aux);

            if (mapAux == null)
            {
                return NotFound();
            }

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

            return mapAux;
        }

        private bool ArticuloExists(int id)
        {
            return (new BS.Articulos(_context).GetOneById!=null);
        }
    }
}
