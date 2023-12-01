using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.Models
{
    public partial class Solicitudes
    {
        public Solicitudes()
        {
            ArticulosSolicitudes = new HashSet<ArticulosSolicitudes>();
        }

        public int IdSolicitud { get; set; }
        public DateTime FecCreacion { get; set; }
        public string EstadoSolicitud { get; set; } 
        public string IdUsuario { get; set; } 
        public string EstadoAprobacion { get; set; }

        public virtual ICollection<ArticulosSolicitudes> ArticulosSolicitudes { get; set; }
    }
}
