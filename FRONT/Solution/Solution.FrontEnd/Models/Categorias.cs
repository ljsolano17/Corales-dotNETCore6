using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.Models
{
    public partial class Categorias
    {
        public Categorias()
        {
            Articulos = new HashSet<Articulos>();
        }

        public int IdCategoria { get; set; }
        public string Tipo { get; set; } = null!;

        public virtual ICollection<Articulos> Articulos { get; set; }
    }
}
