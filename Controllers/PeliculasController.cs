#region Bibliotecas
using ApiServicePeliculas.Datos;
using ApiServicePeliculas.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
#endregion

namespace ApiServicePeliculas.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly BDPeliculasContext _context;

        public PeliculasController(BDPeliculasContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Muestra una lista de todas las películas registradas.
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior deberia aparecer el json con los datos solicitados
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la esquina superior derecha [Autorizar] </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Peliculas>>> Buscar()
        {
            #region Obtener todas las películas registradas

            //Guarda una lista de todas las "Peliculas" en una variable  
            var peliculas = await _context.Peliculas.ToListAsync();


            if (peliculas != null || peliculas?.Count > 0) //Si contiene alguna pelicula...
            {

                return Ok(peliculas); //Me devuelve el objeto con la lista de las peliculas
            }
            else //Si no encontró ninguna...
            {
                return BadRequest("No se encontró ningún dato"); //Mostrar un mensaje indicativo.
            }

            #endregion
        }

        /// <summary>
        /// Permite registrar una nueva película.
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar los datos que se desean guardar (el ID no se tomará en cuenta ya que se genera automáticamente).
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la esquina superior derecha [Autorizar] </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Peliculas>>> Guardar(Peliculas pelicula)
        {
            #region Guarda el nuevo registro en la BD


            pelicula.Id = 0; //Se coloca a su valor por defecto por si el usuario introduce algun dato en la solicitud no tomarlo en cuenta.
            _context.Peliculas.Add(pelicula); //Guarda la pelicula proporcionada por el usuario

            await _context.SaveChangesAsync(); // Guarda los cambios realizados
            return Ok(await _context.Peliculas.ToListAsync()); //Devolver la entidad en forma de lista






            #endregion
        }

        /// <summary>
        /// Permite modificar una película por su número de identificación (ID).
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar los datos que se desean actualizar (Se debe colocar el id de la película que se desea modificar).
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la esquina superior derecha [Autorizar] </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Peliculas>>> Actualizar(Peliculas _pelicula)
        {
            #region Guarda las modificaciones realizadas por el usuario en la BD
            var pelicula = await _context.Peliculas.FindAsync((object)_pelicula.Id);  //Busca la pelicula de forma asincrona cuyo ID es igual al ID proporcionado y lo asigna a una variable

            if (pelicula != null) //Si existe alguna pelicula...
            {
                //Modificar los datos actuales con los datos de los campos proporcionados respectivamente.
                pelicula.Titulo = _pelicula.Titulo;
                pelicula.Descripcion = _pelicula.Descripcion;
                pelicula.Año = _pelicula.Año;
                pelicula.Idactor = _pelicula.Idactor;
                pelicula.Idgenero = _pelicula.Idgenero;
            }
            else //Si no existe...
            {
                return BadRequest("No se encontró ningun dato"); //Devolver el mensaje indicado.
            }

            await _context.SaveChangesAsync(); //Guardar los cambios realizados
            return Ok(await _context.Peliculas.ToListAsync()); //Devolver la entidad en forma de lista
            #endregion
        }

        /// <summary>
        /// Permite borrar una película por su número de identificación (ID).
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar el ID de la película que se desea borrar.
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la izquierda superior derecha [Autorizar] </response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Peliculas>> Borrar(int id)
        {
            #region Borra la pelicula asociada al ID proporcionado

            var pelicula = await _context.Peliculas.FindAsync(id); //Busca la pelicula de forma asincrona cuyo ID es igual al ID proporcionado y lo asigna a una variable

            if (pelicula != null) //Si existe alguna pelicula...
            {
                _context.Peliculas.Remove(pelicula); //Borramos la pelicula
            }
            else //Si no existe...
            {
                return BadRequest("No se encontró ningun dato"); //Devolver el mensaje indicado.

            }

            await _context.SaveChangesAsync(); //Guardar los cambios realizados
            return Ok(await _context.Peliculas.ToListAsync()); //Devolver la entidad en forma de lista
            #endregion
        }

        /// <summary>
        /// Permite borrar una película por su número de identificación (ID) usando un procedimiento almacenado.
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar el ID de la película que se desea borrar.
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la izquierda superior derecha [Autorizar] </response>
        [HttpDelete("BorrarProc/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Peliculas>> BorrarProc(int id)
        {
            #region Borra la pelicula asociada al ID proporcionado usando un procedimiento almacenado

            var pelicula = await _context.Peliculas.FindAsync(id); //Busca la pelicula de forma asincrona cuyo ID es igual al ID proporcionado y lo asigna a una variable

            if (pelicula != null) //Si existe alguna pelicula...
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[EliminarPeliculaProc] @id={id}"); //Ejecuto el procedimiento almacenado pasando como parametro el ID insertado por el usuario.

            }
            else //Si no existe...
            {
                return BadRequest("No se encontró ningún dato"); //Devolver el mensaje indicado.
            }

            await _context.SaveChangesAsync(); //Guardar los cambios realizados
            return Ok(await _context.Peliculas.ToListAsync()); //Devolver la entidad en forma de lista
            #endregion
        }
    }
}
