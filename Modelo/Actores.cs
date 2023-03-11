using System.ComponentModel.DataAnnotations;

namespace ApiServicePeliculas.Modelo
{
   
    public partial class Actores
    {
        [Key]
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }
    }
}
