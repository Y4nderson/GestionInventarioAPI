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
    public class DetalleOrdenPedidoController : ControllerBase
    {
        private readonly DetalleOrdenPedidoRepositorio _detalleOrdenPedidoRepositorio;

        public DetalleOrdenPedidoController(DetalleOrdenPedidoRepositorio detalleOrdenPedidoRepositorio)
        {
            _detalleOrdenPedidoRepositorio = detalleOrdenPedidoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDetalleOrdenPedido()
        {
            var respuesta = await _detalleOrdenPedidoRepositorio.EjecutarSpDetalleOrdenPedido(90, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

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
        public async Task<IActionResult> CrearDetalleOrdenPedido([FromBody] DetalleOrdenPedido detalleOrdenPedido)
        {
            if (!ModelState.IsValid || detalleOrdenPedido == null)
            {
                return BadRequest();
            }

            var respuesta = await _detalleOrdenPedidoRepositorio.EjecutarSpDetalleOrdenPedido(
                detalleOrdenPedido.proceso,
                detalleOrdenPedido.detalleOrdenID,
                detalleOrdenPedido.ordenPedidoID,
                detalleOrdenPedido.productoID,
                detalleOrdenPedido.cantidad,
                detalleOrdenPedido.precioUnitario,
                detalleOrdenPedido.subtotal,
                detalleOrdenPedido.descuento,
                detalleOrdenPedido.neto,
                detalleOrdenPedido.impuesto,
                detalleOrdenPedido.total
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
        public async Task<IActionResult> ActualizarDetalleOrdenPedido([FromBody] DetalleOrdenPedido detalleOrdenPedido)
        {
            if (!ModelState.IsValid || detalleOrdenPedido == null)
            {
                return BadRequest();
            }

            var respuesta = await _detalleOrdenPedidoRepositorio.EjecutarSpDetalleOrdenPedido(
                detalleOrdenPedido.proceso,
                detalleOrdenPedido.detalleOrdenID,
                detalleOrdenPedido.ordenPedidoID,
                detalleOrdenPedido.productoID,
                detalleOrdenPedido.cantidad,
                detalleOrdenPedido.precioUnitario,
                detalleOrdenPedido.subtotal,
                detalleOrdenPedido.descuento,
                detalleOrdenPedido.neto,
                detalleOrdenPedido.impuesto,
                detalleOrdenPedido.total
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

        [HttpDelete("{detalleOrdenID:int}")]
        public async Task<IActionResult> EliminarDetalleOrdenPedido(int detalleOrdenID)
        {
            var respuesta = await _detalleOrdenPedidoRepositorio.EjecutarSpDetalleOrdenPedido(
                3,
                detalleOrdenID,
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
