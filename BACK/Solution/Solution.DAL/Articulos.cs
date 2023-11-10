using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class Articulos : ICRUD<data.Articulos>
    {
        private RepositoryArticulos _repo = null;

        public Articulos(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryArticulos(solutionDbContext);
        }

        public void Delete(data.Articulos t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Articulos> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Articulos GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Articulos t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Articulos t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
        public async Task<IEnumerable<data.Articulos>> GetAllWithAsAsync()
        {
            return await _repo.GetAllWithAsAsync();
            
        }
        public async Task<data.Articulos> GetOneByIdlWithAsAsync(int id)
        {
            return await _repo.GetOneByIdWithAsAsync(id);
        }

    }
}
