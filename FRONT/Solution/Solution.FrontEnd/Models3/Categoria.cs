using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int IdCategoria { get; set; }
        public string Tipo { get; set; } = null!;

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
