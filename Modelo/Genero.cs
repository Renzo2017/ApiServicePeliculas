using System.ComponentModel.DataAnnotations;

namespace ApiServicePeliculas.Modelo
{
    public partial class Genero
    {
        [Key]
        public int Id { get; set; }

        public string? Descripcion { get; set; }

    }
}
