using System;
using System.Collections.Generic;

namespace Solution.API.W.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
}
