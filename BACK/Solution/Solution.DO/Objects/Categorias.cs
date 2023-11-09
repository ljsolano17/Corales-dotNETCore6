using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.DO.Objects
{
    public class Categorias
    {
        public int IdCategoria { get; set; }

        public string Tipo { get; set; } = null!;

        public virtual ICollection<Articulos> Articulos { get; set; } = new List<Articulos>();
    }
}
