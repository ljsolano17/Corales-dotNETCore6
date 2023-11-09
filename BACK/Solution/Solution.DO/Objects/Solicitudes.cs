using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.DO.Objects
{
    public class Solicitudes
    {
        public int IdSolicitud { get; set; }

        public DateTime FecCreacion { get; set; }

        public string EstadoSolicitud { get; set; } = null!;

        public string IdUsuario { get; set; } = null!;

        public string? EstadoAprobacion { get; set; }

        public virtual ICollection<ArticulosSolicitud> ArticulosSolicitud { get; set; } = new List<ArticulosSolicitud>();
    }
}
