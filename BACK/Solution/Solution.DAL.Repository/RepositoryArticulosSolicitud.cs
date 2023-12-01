using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using Solution.DO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public class RepositoryArticulosSolicitud : Repository<data.ArticulosSolicitud>, IRepositoryArticulosSolicitud
    {
        public RepositoryArticulosSolicitud(SolutionDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<data.ArticulosSolicitud>> GetAllWithAsAsync()
        {
            return await _db.ArticulosSolicitud
                .Include(m => m.IdSolicitudNavigation)
                .Include(m => m.IdSolicitudNavigation)//posible error
                .ToListAsync();
        }


        public async Task<data.ArticulosSolicitud> GetOneByIdWithAsAsync(int id)
        {
            return await _db.ArticulosSolicitud
                .Include(m => m.IdArticuloNavigation)
                .Include(m => m.IdSolicitudNavigation)
                .SingleOrDefaultAsync(m => m.IdArticuloSolicitud == id);
        }

       

        private SolutionDbContext _db
        {
            get {return dbContext as SolutionDbContext;} 
        }

        
    }
}
