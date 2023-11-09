using System;
using System.Collections.Generic;

namespace Solution.API.W.Models;

public partial class Solicitude
{
    public int IdSolicitud { get; set; }

    public DateTime FecCreacion { get; set; }

    public string EstadoSolicitud { get; set; } = null!;

    public string IdUsuario { get; set; } = null!;

    public string? EstadoAprobacion { get; set; }

    public virtual ICollection<ArticulosSolicitud> ArticulosSolicituds { get; set; } = new List<ArticulosSolicitud>();
}
