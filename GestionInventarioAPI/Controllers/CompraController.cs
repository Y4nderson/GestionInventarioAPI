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
    public class ComprasInventarioController : ControllerBase
    {
        private readonly CompraRepositorio _comprasInventarioRepositorio;

        public ComprasInventarioController(CompraRepositorio comprasInventarioRepositorio)
        {
            _comprasInventarioRepositorio = comprasInventarioRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerComprasInventario()
        {
            var respuesta = await _comprasInventarioRepositorio.EjecutarSpComprasInventario(90, 0, 0, 0, DateTime.Now, 0, 0, 0);

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
        public async Task<IActionResult> CrearCompraInventario([FromBody] Compra compraInventario)
        {
            if (!ModelState.IsValid || compraInventario == null)
            {
                return BadRequest();
            }

            var respuesta = await _comprasInventarioRepositorio.EjecutarSpComprasInventario(
                compraInventario.proceso,
                compraInventario.compraID,
                compraInventario.ordenPedidoID,
                compraInventario.proveedorID,
                compraInventario.fechaCompra,
                compraInventario.total,
                compraInventario.usuarioID,
                compraInventario.almacenID
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
        public async Task<IActionResult> ActualizarCompraInventario([FromBody] Compra compraInventario)
        {
            if (!ModelState.IsValid || compraInventario == null)
            {
                return BadRequest();
            }

            var respuesta = await _comprasInventarioRepositorio.EjecutarSpComprasInventario(
                compraInventario.proceso,
                compraInventario.compraID,
                compraInventario.ordenPedidoID,
                compraInventario.proveedorID,
                compraInventario.fechaCompra,
                compraInventario.total,
                compraInventario.usuarioID,
                compraInventario.almacenID
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

        [HttpDelete("{compraID:int}")]
        public async Task<IActionResult> EliminarCompraInventario(int compraID)
        {
            var respuesta = await _comprasInventarioRepositorio.EjecutarSpComprasInventario(
                3,
                compraID,
                0, 0, DateTime.Now, 0, 0, 0
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
