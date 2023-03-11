#region Bibliotecas
using ApiServicePeliculas.Datos;
using ApiServicePeliculas.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
#endregion

namespace ApiServicePeliculas.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BDPeliculasContext _context;

        private IConfiguration _configuration;

        public LoginController(BDPeliculasContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }


        /// <summary>
        /// Iniciar sesión en la Api (Si los datos son validos obtendrá un token).
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior deberia aparecer el json con los datos solicitados
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Validar(Usuarios usuario)
        {
            #region Validamos que los datos introducimos coincidan con los datos registrados en la Tabla usuarios
            //Validamos que el usuario y la contraseña sean correctas
            var usuarioValido = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuario.NombreUsuario && u.Contraseña == usuario.Contraseña);

            if (usuarioValido == null) //Si no encontró ningun dato con esas credenciales...
            {
                return BadRequest("Nombre de usuario o contraseña incorrectos."); //Devolver mensaje indicado.
            }

            string jwtToken = GenerarToken(usuarioValido); //Asignamos el token generado a la variable

            return Ok(new { token = jwtToken}); //Devuelvo el token obtenido
            #endregion
        }

        private string GenerarToken(Usuarios usuario)
        {
            #region Genera un token y lo serializa en un string
            var claim = new[] //Definimos las propiedades del claim con los datos del usuario
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)

            };

            //Obtenemos los valores de la llave del archivo de configuracion json.
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));

            //Definimos las credenciales asignadole la llave y el algoritmo de codificacion que utilizará.
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha512Signature);

            //Inicializamos las propiedades del token de seguridad
            var tokenSeguridad = new JwtSecurityToken(
                 claims: claim, //Asignamos el claim
                 expires: DateTime.Now.AddMinutes(60), //Duracion del token
                 signingCredentials: credenciales); //Credenciales del token

            string token = new JwtSecurityTokenHandler().WriteToken(tokenSeguridad); //Serializamos el token en una string

            return token; //Devolver el token serializado.
            #endregion
        }
    }
}
