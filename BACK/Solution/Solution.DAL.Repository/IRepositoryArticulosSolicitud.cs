
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryArticulosSolicitud : IRepository<data.ArticulosSolicitud>
    {
        Task<IEnumerable<data.ArticulosSolicitud>> GetAllWithAsAsync();
        Task<data.ArticulosSolicitud> GetOneByIdWithAsAsync(int id);
    }
}
