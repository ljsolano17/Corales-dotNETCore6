﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryArticulos: IRepository<data.Articulos>
    {
        Task<IEnumerable<data.Articulos>> GetAllWithAsAsync();
        Task<data.Articulos> GetOneByIdWithAsAsync(int id);
        
    }
}
