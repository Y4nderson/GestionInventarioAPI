using GestionInventarioAPI.Modelos;
using GestionInventarioAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GestionInventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosRepositorio _usuariosRepositorio;

        public UsuariosController(UsuariosRepositorio usuariosRepositorio)
        {
            _usuariosRepositorio = usuariosRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var respuesta = await _usuariosRepositorio.EjecutarSpUsuarios(90, 0, "", "", "", "", 0, "", "", DateTime.Now, DateTime.Now);

            if (respuesta != null && respuesta.Tables.Count > 0)
            {
                var listaResultado = new List<Dictionary<string, object>>();

                foreach (DataRow fila in respuesta.Tables[0].Rows)
                {
                    var filaDatos = new Dictionary<string, object>();

                    foreach (DataColumn column in respuesta.Tables[0].Columns)
                    {
                        filaDatos[column.ColumnName] = fila[column];
                    }

                    listaResultado.Add(filaDatos);
                }

                return Ok(listaResultado);
            }
            else
            {
                return NotFound("No se encontraron datos");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid || usuario == null)
            {
                return BadRequest();
            }

            var respuesta = await _usuariosRepositorio.EjecutarSpUsuarios(
                usuario.proceso,
                usuario.usuarioID,
                usuario.nombreUsuario,
                usuario.contrasenaHash,
                usuario.correoElectronico,
                usuario.nombreCompleto,
                usuario.estado,
                usuario.rol,
                usuario.permisos,
                usuario.fechaDeCreacion,
                usuario.ultimoAcceso
            );

            if (respuesta != null && respuesta.HasErrors == false)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Error del servidor");
            }
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid || usuario == null)
            {
                return BadRequest();
            }

            var respuesta = await _usuariosRepositorio.EjecutarSpUsuarios(
                usuario.proceso,
                usuario.usuarioID,
                usuario.nombreUsuario,
                usuario.contrasenaHash,
                usuario.correoElectronico,
                usuario.nombreCompleto,
                usuario.estado,
                usuario.rol,
                usuario.permisos,
                usuario.fechaDeCreacion,
                usuario.ultimoAcceso
            );

            if (respuesta != null && respuesta.HasErrors == false)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Error del servidor");
            }
        }

        [HttpDelete("{usuarioID:int}")]
        public async Task<IActionResult> EliminarUsuario(int usuarioID)
        {
            var respuesta = await _usuariosRepositorio.EjecutarSpUsuarios(
                3,
                usuarioID,
                "", "", "", "", 0, "", "", DateTime.Now, DateTime.Now
            );

            if (respuesta != null && respuesta.HasErrors == false)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Error del servidor");
            }
        }
    }
}
