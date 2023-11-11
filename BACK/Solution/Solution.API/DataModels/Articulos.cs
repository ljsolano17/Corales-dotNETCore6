﻿namespace Solution.API.DataModels
{
    public class Articulos
    {
        public int IdArticulo { get; set; }

        public string? NombreCientifico { get; set; }

        public string? Familia { get; set; }

        public string? Tipo { get; set; }

        public string? NombreComun { get; set; }

        public string? Dificultad { get; set; }

        public string? Temperamento { get; set; }

        public string? Color { get; set; }

        public string? Dieta { get; set; }

        public string? TamanoMax { get; set; }

        public string? Origen { get; set; }

        public string? TamanoMinPecera { get; set; }

        public string? ModificadoPor { get; set; }

        public DateTime? FecModificacion { get; set; }

        public string? ImagePath { get; set; }

        public int? IdCategoria { get; set; }

       // public virtual ICollection<ArticulosSolicitud> ArticulosSolicitud { get; set; } = new List<ArticulosSolicitud>();

        public virtual Categorias? IdCategoriaNavigation { get; set; }
    }
}
