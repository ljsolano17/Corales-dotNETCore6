using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using datamodels = Solution.API.DataModels;
using data = Solution.DO.Objects;
using Solution.DAL.EF;
using AutoMapper;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosSolicitudController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public ArticulosSolicitudController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ArticulosSolicitud
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.ArticulosSolicitud>>> GetArticulosSolicitud()
        {
            var aux = await new BS.ArticulosSolicitud(_context).GetAllWithAsync();
            var mapAux = _mapper.Map<IEnumerable<data.ArticulosSolicitud>,IEnumerable<datamodels.ArticulosSolicitud>>(aux).ToList();

            return mapAux;
            
        }

        // GET: api/ArticulosSolicitud/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.ArticulosSolicitud>> GetArticulosSolicitud(int id)
        {
            var aux = await new BS.ArticulosSolicitud(_context).GetOneByIdWithAsync(id);
            var mapAux = _mapper.Map<data.ArticulosSolicitud, datamodels.ArticulosSolicitud>(aux);


            if (mapAux == null)
            {
                return NotFound();
            }

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


            return mapAux;
        }

        private bool ArticulosSolicitudExists(int id)
        {
            return (new BS.ArticulosSolicitud(_context).GetOneById(id)!=null);
        }
    }
}
