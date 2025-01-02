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
    public class DetalleNotaCreditoController : ControllerBase
    {
        private readonly DetalleNotaCreditoRepositorio _detalleNotaCreditoRepositorio;

        public DetalleNotaCreditoController(DetalleNotaCreditoRepositorio detalleNotaCreditoRepositorio)
        {
            _detalleNotaCreditoRepositorio = detalleNotaCreditoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDetalleNotaCredito()
        {
            var respuesta = await _detalleNotaCreditoRepositorio.EjecutarSpDetalleNotaCredito(90, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

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
        public async Task<IActionResult> CrearDetalleNotaCredito([FromBody] DetalleNotaCredito detalleNotaCredito)
        {
            if (!ModelState.IsValid || detalleNotaCredito == null)
            {
                return BadRequest();
            }

            var respuesta = await _detalleNotaCreditoRepositorio.EjecutarSpDetalleNotaCredito(
                detalleNotaCredito.proceso,
                detalleNotaCredito.detalleNotaID,
                detalleNotaCredito.notaCreditoID,
                detalleNotaCredito.productoID,
                detalleNotaCredito.cantidad,
                detalleNotaCredito.precioUnitario,
                detalleNotaCredito.subtotal,
                detalleNotaCredito.descuento,
                detalleNotaCredito.neto,
                detalleNotaCredito.impuesto,
                detalleNotaCredito.total
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
        public async Task<IActionResult> ActualizarDetalleNotaCredito([FromBody] DetalleNotaCredito detalleNotaCredito)
        {
            if (!ModelState.IsValid || detalleNotaCredito == null)
            {
                return BadRequest();
            }

            var respuesta = await _detalleNotaCreditoRepositorio.EjecutarSpDetalleNotaCredito(
                detalleNotaCredito.proceso,
                detalleNotaCredito.detalleNotaID,
                detalleNotaCredito.notaCreditoID,
                detalleNotaCredito.productoID,
                detalleNotaCredito.cantidad,
                detalleNotaCredito.precioUnitario,
                detalleNotaCredito.subtotal,
                detalleNotaCredito.descuento,
                detalleNotaCredito.neto,
                detalleNotaCredito.impuesto,
                detalleNotaCredito.total
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

        [HttpDelete("{detalleNotaID:int}")]
        public async Task<IActionResult> EliminarDetalleNotaCredito(int detalleNotaID)
        {
            var respuesta = await _detalleNotaCreditoRepositorio.EjecutarSpDetalleNotaCredito(
                3,
                detalleNotaID,
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
