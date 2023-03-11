using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiServicePeliculas.Modelo
{
    public class Usuarios
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        public string? NombreUsuario { get; set; }

        public string? Contraseña { get; set;}
    }
}
