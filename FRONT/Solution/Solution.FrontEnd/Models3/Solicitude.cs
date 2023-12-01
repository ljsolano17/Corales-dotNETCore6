using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.Models
{
    public partial class Solicitude
    {
        public Solicitude()
        {
            ArticulosSolicituds = new HashSet<ArticulosSolicitud>();
        }

        public int IdSolicitud { get; set; }
        public DateTime FecCreacion { get; set; }
        public string EstadoSolicitud { get; set; } = null!;
        public string IdUsuario { get; set; } = null!;
        public string? EstadoAprobacion { get; set; }

        public virtual ICollection<ArticulosSolicitud> ArticulosSolicituds { get; set; }
    }
}
