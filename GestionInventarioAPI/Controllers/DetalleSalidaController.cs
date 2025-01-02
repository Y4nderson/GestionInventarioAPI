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
    public class DetalleSalidaController : ControllerBase
    {
        private readonly DetalleSalidaRepositorio _detalleSalidaRepositorio;

        public DetalleSalidaController(DetalleSalidaRepositorio detalleSalidaRepositorio)
        {
            _detalleSalidaRepositorio = detalleSalidaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDetalleSalida()
        {
            var respuesta = await _detalleSalidaRepositorio.EjecutarSpDetalleSalida(90, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

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
        public async Task<IActionResult> CrearDetalleSalida([FromBody] DetalleSalida detalleSalida)
        {
            if (!ModelState.IsValid || detalleSalida == null)
            {
                return BadRequest();
            }

            var respuesta = await _detalleSalidaRepositorio.EjecutarSpDetalleSalida(
                detalleSalida.proceso,
                detalleSalida.detalleSalidaID,
                detalleSalida.salidaAlmacenID,
                detalleSalida.productoID,
                detalleSalida.cantidad,
                detalleSalida.precioUnitario,
                detalleSalida.subtotal,
                detalleSalida.descuento,
                detalleSalida.neto,
                detalleSalida.impuesto,
                detalleSalida.total
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
        public async Task<IActionResult> ActualizarDetalleSalida([FromBody] DetalleSalida detalleSalida)
        {
            if (!ModelState.IsValid || detalleSalida == null)
            {
                return BadRequest();
            }

            var respuesta = await _detalleSalidaRepositorio.EjecutarSpDetalleSalida(
                detalleSalida.proceso,
                detalleSalida.detalleSalidaID,
                detalleSalida.salidaAlmacenID,
                detalleSalida.productoID,
                detalleSalida.cantidad,
                detalleSalida.precioUnitario,
                detalleSalida.subtotal,
                detalleSalida.descuento,
                detalleSalida.neto,
                detalleSalida.impuesto,
                detalleSalida.total
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

        [HttpDelete("{detalleSalidaID:int}")]
        public async Task<IActionResult> EliminarDetalleSalida(int detalleSalidaID)
        {
            var respuesta = await _detalleSalidaRepositorio.EjecutarSpDetalleSalida(
                3,
                detalleSalidaID,
                0, 0, 0, 0, 0, 0, 0, 0, 0
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
