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
    public class OrdenPedidoController : ControllerBase
    {
        private readonly OrdenPedidoRepositorio _ordenPedidoRepositorio;

        public OrdenPedidoController(OrdenPedidoRepositorio ordenPedidoRepositorio)
        {
            _ordenPedidoRepositorio = ordenPedidoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerOrdenesPedido()
        {
            var respuesta = await _ordenPedidoRepositorio.EjecutarSpOrdenPedido(90, 0, 0, DateTime.Now, 0, 0);

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
        public async Task<IActionResult> CrearOrdenPedido([FromBody] OrdenPedido ordenPedido)
        {
            if (!ModelState.IsValid || ordenPedido == null)
            {
                return BadRequest();
            }

            var respuesta = await _ordenPedidoRepositorio.EjecutarSpOrdenPedido(
                ordenPedido.proceso,
                ordenPedido.ordenPedidoID,
                ordenPedido.proveedorID,
                ordenPedido.fechaOrden,
                ordenPedido.total,
                ordenPedido.usuarioID
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
        public async Task<IActionResult> ActualizarOrdenPedido([FromBody] OrdenPedido ordenPedido)
        {
            if (!ModelState.IsValid || ordenPedido == null)
            {
                return BadRequest();
            }

            var respuesta = await _ordenPedidoRepositorio.EjecutarSpOrdenPedido(
                ordenPedido.proceso,
                ordenPedido.ordenPedidoID,
                ordenPedido.proveedorID,
                ordenPedido.fechaOrden,
                ordenPedido.total,
                ordenPedido.usuarioID
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

        [HttpDelete("{ordenPedidoID:int}")]
        public async Task<IActionResult> EliminarOrdenPedido(int ordenPedidoID)
        {
            var respuesta = await _ordenPedidoRepositorio.EjecutarSpOrdenPedido(
                3,
                ordenPedidoID,
                0, DateTime.Now, 0, 0
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
