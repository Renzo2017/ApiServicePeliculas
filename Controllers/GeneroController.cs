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
    public class GeneroController : ControllerBase
    {
        private readonly BDPeliculasContext _context;

        public GeneroController(BDPeliculasContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Muestra una lista de todos los generos de peliculas registradas.
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior deberia aparecer el json con los datos solicitados
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la esquina superior derecha [Autorizar] </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Genero>>> Buscar()
        {
            #region Obtener todos los Generos registradas
            var genero = await _context.Generos.ToListAsync();   //Guarda una lista de todos los "Generos" registrados en una variable  

            if (genero != null || genero?.Count > 0) //Si contiene algun genero...
            {
                return Ok(genero); //Me devuelve el objeto con la lista de generos
            }
            else //Si no encontró ninguno...
            {
                return BadRequest("No se encontró ningun dato"); //Mostrar un mensaje indicativo.
            }
            #endregion
        }

        /// <summary>
        /// Permite registrar un nuevo género.
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar los datos que se desean guardar (el ID no se tomará en cuenta ya que se genera automáticamente).
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la esquina superior derecha [Autorizar] </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Genero>>> Guardar(Genero genero)
        {
            #region Guarda el nuevo registro en la BD
            
            genero.Id = 0; //Se coloca a su valor por defecto por si el usuario introduce algun dato en la solicitud no tomarlo en cuenta.
            
            _context.Generos.Add(genero); //Guarda el genero proporcionado por el usuario
            
            await _context.SaveChangesAsync();  // Guarda los cambios realizados
           
            return Ok(await _context.Generos.ToListAsync()); //Devolver la entidad en forma de lista
            #endregion
        }

        /// <summary>
        /// Permite modificar un género por su número de identificación (ID).
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar los datos que se desean actualizar (Se debe colocar el id del género que se desea modificar).
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la esquina superior derecha [Autorizar] </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Genero>>> Actualidar (Genero _genero)
        {
            #region Guarda las modificaciones realizadas por el usuario en la BD

            var genero = await _context.Generos.FindAsync(_genero.Id); //Busca el genero de forma asincrona cuyo ID es igual al ID proporcionado y lo asigna a una variable

            if (genero != null) //Si existe algun genero...
            {
                //Modificar los datos actuales con los datos de los campos proporcionados respectivamente.
                genero.Descripcion = _genero.Descripcion;
             
               
            }
            else //Si no existe...
            {
                return BadRequest("No se encontró ningun dato");  //Devolver el mensaje indicado.
            }
            await _context.SaveChangesAsync();  //Guardar los cambios realizados
            return Ok(await _context.Generos.ToListAsync()); //Devolver la entidad en forma de lista
            #endregion
        }

        /// <summary>
        /// Permite borrar un género por su número de identificación (ID).
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar el ID del género que se desea borrar.
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la izquierda superior derecha [Autorizar] </response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Genero>>> Borrar (int id)
        {
            #region Borra al actor asociada al ID Proporcionado

            var genero = await _context.Generos.FindAsync(id); //Busca el genero de forma asincrona cuyo ID es igual al ID proporcionado y lo asigna a una variable
             
            if (genero != null) //Si existe algun genero...
            {
                _context.Generos.Remove(genero); //Borramos el genero
            }
            else //Si no existe...
            {
                return BadRequest("No se encotró el ningun dato"); //Devolver el mensaje indicado.
            }

            await _context.SaveChangesAsync(); //Guardar los cambios realizados
            return Ok(await _context.Generos.ToListAsync());  //Devolver la entidad en forma de lista

            #endregion
        }

        /// <summary>
        /// Permite borrar un género por su número de identificación (ID) usando un procedimiento almacenado.
        /// </summary>
        /// <remarks>
        /// Primero preciona el botón Try it Out (Pruébalo) luego el botón ejecutar en azul. En la parte inferior se debe suministrar el ID del género que se desea borrar.
        /// </remarks>
        /// <response code="401">Debe iniciar sesión en el login y el token devuelto colocarlo en el botón verde de la izquierda superior derecha [Autorizar] </response>
        [HttpDelete("BorrarProc/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Genero>>> BorrarProc(int id)
        {
            #region Borra al actor asociada al ID proporcionado usando un procedimiento almacenado.

            var genero = await _context.Generos.FindAsync(id); //Busca el género de forma asíncrona cuyo ID es igual al ID proporcionado y lo asigna a una variable.

            if (genero != null) //Si existe algún género...
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[EliminarGeneroProc] @id={id}"); //Ejecuto el procedimiento almacenado pasando como parametro el ID insertado por el usuario.
            }
            else //Si no existe...
            {
                return BadRequest("No se encotró ningún dato"); //Devolver el mensaje indicado.
            }

            await _context.SaveChangesAsync(); //Guardar los cambios realizados.
            return Ok(await _context.Generos.ToListAsync());  //Devolver la entidad en forma de lista.

            #endregion
        }
    }
}
