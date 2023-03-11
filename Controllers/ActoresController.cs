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

    public class ActoresController : ControllerBase
    {
        private readonly BDPeliculasContext _context;

        public ActoresController(BDPeliculasContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Muestra una lista de todos los actores registrados.
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior deberia aparecer el json con los datos solicitados
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el boton verde de la esquina superior derecha [Autorizar] </response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Actores>>> Buscar()
        {
            #region Obtener todos los Actores registradas

            //Guarda una lista de todos los "Actores" registrados en una variable  
            var actores = await _context.Actores.ToListAsync();


            if (actores != null || actores?.Count > 0) //Si contiene algun actor...
            {

                return Ok(actores); //Me devuelve el objeto con la lista de los actores
            }
            else //Si no encontró ninguno...
            {
                return BadRequest("No se encontró ningún dato"); //Mostrar un mensaje indicativo.
            }

            #endregion
        }

        /// <summary>
        /// Permite registrar un nuevo actor.
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se deben suministrar los datos que se desean guardar (el ID no se tomará en cuenta ya que se genera automaticamente).
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el boton verde de la esquina superior derecha [Autorizar] </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Actores>>> Guardar(Actores actor)
        {
            #region Guarda el nuevo registro en la BD
           
            actor.Id = 0; //Se coloca a su valor por defecto por si el usuario introduce algun dato en la solicitud no tomarlo en cuenta.
            _context.Actores.Add(actor); //Guarda el actor proporcionado por el usuario

            await _context.SaveChangesAsync(); // Guarda los cambios realizados
            return Ok(await _context.Actores.ToListAsync()); //Devolver la entidad en forma de lista
            #endregion
        }

        /// <summary>
        /// Permite modificar un actor por su número de identificación (ID).
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar los datos que se desean actualizar (Se debe colocar el id del actor que se desea modificar).
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el boton verde de la esquina superior derecha [Autorizar] </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Actores>>> Actualizar(Actores _actores)
        {
            #region Guarda las modificaciones realizadas por el usuario en la BD
            var actores = await _context.Actores.FindAsync(_actores.Id);  //Busca el actor de forma asincrona cuyo ID es igual al ID proporcionado y lo asigna a una variable

            if (actores != null) //Si existe algun actor...
            {
                //Modificar los datos actuales con los datos de los campos proporcionados respectivamente.
                actores.Nombre = _actores.Nombre;
                actores.Apellido = _actores.Apellido;

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
        /// Permite borrar un actor por su número de identificación (ID).
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar el ID del actor que se desea eliminar.
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el boton verde de la izquierda superior derecha [Autorizar] </response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Actores>>> Borrar(int id)
        {
            #region Borra al actor asociada al ID proporcionado

            var actores = await _context.Actores.FindAsync(id); //Busca el actor de forma asincrona cuyo ID es igual al ID proporcionado y lo asigna a una variable

            if (actores != null) //Si existe algun actor...
            {
                _context.Actores.Remove(actores);  //Borramos el actor
            }
            else //Si no existe...
            {
                return BadRequest("No se encontró ningun dato"); //Devolver el mensaje indicado.
            }

            await _context.SaveChangesAsync(); //Guardar los cambios realizados
            return Ok(await _context.Actores.ToListAsync());  //Devolver la entidad en forma de lista

            #endregion
        }

        /// <summary>
        /// Permite borrar un actor por su número de identificación (ID) usando un procedimiento almacenado.
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar el ID del actor que se desea eliminar.
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el boton verde de la izquierda superior derecha [Autorizar] </response>
        [HttpDelete("BorrarProc/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Actores>>> BorrarProc(int id)
        {
            #region Borra al actor asociada al ID proporcionado usando un procedimiento almacenado.
            var actores = await _context.Actores.FindAsync(id);  //Busca el actor de forma asincrona cuyo ID es igual al ID proporcionado y lo asigna a una variable

            if (actores != null) //Si existe algun actor...
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[EliminarActorProc] @id={id}"); //Ejecuto el procedimiento almacenado pasando como parametro el ID insertado por el usuario.
            }
            else //Si no existe...
            {
                return BadRequest("No se encontró ningún dato"); //Devolver el mensaje indicado.
            }
            await _context.SaveChangesAsync(); //Guardar los cambios realizados
            return Ok(await _context.Actores.ToListAsync()); //Devolver la entidad en forma de lista
            #endregion
        }
    }
}
