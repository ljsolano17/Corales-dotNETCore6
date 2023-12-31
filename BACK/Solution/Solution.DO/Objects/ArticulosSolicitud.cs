﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.DO.Objects
{
    public class ArticulosSolicitud
    {
        public int IdArticuloSolicitud { get; set; }

        public int IdSolicitud { get; set; }

        public int IdArticulo { get; set; }

        public int? Cantidad { get; set; }

        public virtual Articulos? IdArticuloNavigation { get; set; } 

        public virtual Solicitudes? IdSolicitudNavigation { get; set; }
    }
}
