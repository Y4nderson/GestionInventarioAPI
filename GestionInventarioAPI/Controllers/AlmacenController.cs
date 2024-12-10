using GestionInventarioAPI.Modelos;
using GestionInventarioAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GestionInventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController : ControllerBase
    {

        private readonly AlmacenRepositorio _almacenRepositorio;
        public AlmacenController(AlmacenRepositorio almacenRepositorio)
        {
            _almacenRepositorio = almacenRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAlmacen()
        {

            var respuesta = await _almacenRepositorio.EjecutarSpAlmacen(90, 0, "", "", 0);

            if (respuesta != null && respuesta.Tables.Count > 0)
            {

                var ListaResultado = new List<Dictionary<string, object>>();

                foreach (DataRow fila in respuesta.Tables[0].Rows) 
                { 
                
                    var filaDatos = new Dictionary<string, object>();

                    foreach(DataColumn column in respuesta.Tables[0].Columns)
                    {

                        filaDatos[column.ColumnName] = fila[column];

                    }

                    ListaResultado.Add(filaDatos);
                }

                return Ok(ListaResultado);
                
            }
            else
            {
                return NotFound("No se encontraron datos"); ;
            }

        }

        [HttpPost]
        public async Task<IActionResult> CrearAlmacen([FromBody] Almacen almacen)
        {

            if (!ModelState.IsValid || almacen == null)
            {
                return BadRequest();
            }

            var respuesta = await _almacenRepositorio.EjecutarSpAlmacen(
                almacen.proceso,
                almacen.almacenID,
                almacen.nombreAlmacen,
                almacen.ubicacion,
                almacen.usuarioID

                );


            if (respuesta != null && respuesta.HasErrors == false)
            {
                return Ok();

            }
            else
            {
                return StatusCode(500, "Error del servidor ");
            }

        }


        [HttpPut]
        public async Task<IActionResult> ActualizarAlmacen([FromBody] Almacen almacen)
        {

            if (!ModelState.IsValid || almacen == null)
            {
                return BadRequest();
            }

            var respuesta = await _almacenRepositorio.EjecutarSpAlmacen(
                almacen.proceso,
                almacen.almacenID,
                almacen.nombreAlmacen,
                almacen.ubicacion,
                almacen.usuarioID

                );


            if (respuesta != null && respuesta.HasErrors == false)
            {
                return Ok();

            }
            else
            {
                return StatusCode(500, "Error del servidor ");
            }

        }


        [HttpDelete("{almacenID:int}")]
        public async Task<IActionResult> EliminarAlmacen(int almacenID)
        {


            var respuesta = await _almacenRepositorio.EjecutarSpAlmacen(
                3,
                almacenID,
                "",
                "",
                0

                );


            if (respuesta != null && respuesta.HasErrors == false)
            {
                return Ok();

            }
            else
            {
                return StatusCode(500, "Error del servidor ");
            }

        }


    }
}
