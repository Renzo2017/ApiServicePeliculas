using System.ComponentModel.DataAnnotations;

namespace ApiServicePeliculas.Modelo
{
    public partial class Peliculas
    {
        [Key]
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public int? Año { get; set; }

        public int Idactor { get; set; }

        public int Idgenero { get; set; }

    }
}
