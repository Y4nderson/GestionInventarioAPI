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
    public class NotaCreditoController : ControllerBase
    {
        private readonly NotaCreditoRepositorio _notaCreditoRepositorio;

        public NotaCreditoController(NotaCreditoRepositorio notaCreditoRepositorio)
        {
            _notaCreditoRepositorio = notaCreditoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerNotasCredito()
        {
            var respuesta = await _notaCreditoRepositorio.EjecutarSpNotaCredito(90, 0, 0, "", DateTime.Now, 0, 0);

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
        public async Task<IActionResult> CrearNotaCredito([FromBody] NotaCredito notaCredito)
        {
            if (!ModelState.IsValid || notaCredito == null)
            {
                return BadRequest();
            }

            var respuesta = await _notaCreditoRepositorio.EjecutarSpNotaCredito(
                notaCredito.proceso,
                notaCredito.notaCreditoID,
                notaCredito.compraID,
                notaCredito.motivo,
                notaCredito.fechaNota,
                notaCredito.total,
                notaCredito.usuarioID
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
        public async Task<IActionResult> ActualizarNotaCredito([FromBody] NotaCredito notaCredito)
        {
            if (!ModelState.IsValid || notaCredito == null)
            {
                return BadRequest();
            }

            var respuesta = await _notaCreditoRepositorio.EjecutarSpNotaCredito(
                notaCredito.proceso,
                notaCredito.notaCreditoID,
                notaCredito.compraID,
                notaCredito.motivo,
                notaCredito.fechaNota,
                notaCredito.total,
                notaCredito.usuarioID
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

        [HttpDelete("{notaCreditoID:int}")]
        public async Task<IActionResult> EliminarNotaCredito(int notaCreditoID)
        {
            var respuesta = await _notaCreditoRepositorio.EjecutarSpNotaCredito(
                3,
                notaCreditoID,
                0, "", DateTime.Now, 0, 0
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
