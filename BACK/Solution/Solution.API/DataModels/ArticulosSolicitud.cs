namespace Solution.API.DataModels
{
    public class ArticulosSolicitud
    {
        public int IdArticuloSolicitud { get; set; }

        public int IdSolicitud { get; set; }

        public int IdArticulo { get; set; }

        public int? Cantidad { get; set; }

        public virtual Articulos IdArticuloNavigation { get; set; } = null!;

        public virtual Solicitudes IdSolicitudNavigation { get; set; } = null!;
    }
}
