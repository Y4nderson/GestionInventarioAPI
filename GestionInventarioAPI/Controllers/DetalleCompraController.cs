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
    public class DetalleCompraController : ControllerBase
    {
        private readonly DetalleCompraRepositorio _detalleCompraRepositorio;

        public DetalleCompraController(DetalleCompraRepositorio detalleCompraRepositorio)
        {
            _detalleCompraRepositorio = detalleCompraRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDetalleCompra()
        {
            var respuesta = await _detalleCompraRepositorio.EjecutarSpDetalleCompra(90, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

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
        public async Task<IActionResult> CrearDetalleCompra([FromBody] DetalleCompra detalleCompra)
        {
            if (!ModelState.IsValid || detalleCompra == null)
            {
                return BadRequest();
            }

            var respuesta = await _detalleCompraRepositorio.EjecutarSpDetalleCompra(
                detalleCompra.proceso,
                detalleCompra.detalleCompraID,
                detalleCompra.compraID,
                detalleCompra.productoID,
                detalleCompra.cantidad,
                detalleCompra.precioUnitario,
                detalleCompra.subtotal,
                detalleCompra.descuento,
                detalleCompra.neto,
                detalleCompra.impuesto,
                detalleCompra.total
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
        public async Task<IActionResult> ActualizarDetalleCompra([FromBody] DetalleCompra detalleCompra)
        {
            if (!ModelState.IsValid || detalleCompra == null)
            {
                return BadRequest();
            }

            var respuesta = await _detalleCompraRepositorio.EjecutarSpDetalleCompra(
                detalleCompra.proceso,
                detalleCompra.detalleCompraID,
                detalleCompra.compraID,
                detalleCompra.productoID,
                detalleCompra.cantidad,
                detalleCompra.precioUnitario,
                detalleCompra.subtotal,
                detalleCompra.descuento,
                detalleCompra.neto,
                detalleCompra.impuesto,
                detalleCompra.total
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

        [HttpDelete("{detalleCompraID:int}")]
        public async Task<IActionResult> EliminarDetalleCompra(int detalleCompraID)
        {
            var respuesta = await _detalleCompraRepositorio.EjecutarSpDetalleCompra(
                3,
                detalleCompraID,
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
