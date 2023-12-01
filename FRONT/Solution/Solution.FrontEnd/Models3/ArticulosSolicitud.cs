using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.Models
{
    public partial class ArticulosSolicitud
    {
        public int IdArticuloSolicitud { get; set; }
        public int IdSolicitud { get; set; }
        public int IdArticulo { get; set; }
        public int? Cantidad { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        public virtual Solicitude IdSolicitudNavigation { get; set; } = null!;
    }
}
