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
    public class SalidasAlmacenController : ControllerBase
    {
        private readonly SalidasAlmacenRepositorio _salidasAlmacenRepositorio;

        public SalidasAlmacenController(SalidasAlmacenRepositorio salidasAlmacenRepositorio)
        {
            _salidasAlmacenRepositorio = salidasAlmacenRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSalidasAlmacen()
        {
            var respuesta = await _salidasAlmacenRepositorio.EjecutarSpSalidasAlmacen(90, 0, 0, 0, DateTime.Now, 0, 0);

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
        public async Task<IActionResult> CrearSalidaAlmacen([FromBody] SalidaAlmacen salidaAlmacen)
        {
            if (!ModelState.IsValid || salidaAlmacen == null)
            {
                return BadRequest();
            }

            var respuesta = await _salidasAlmacenRepositorio.EjecutarSpSalidasAlmacen(
                salidaAlmacen.proceso,
                salidaAlmacen.salidaAlmacenID,
                salidaAlmacen.almacenOrigenID,
                salidaAlmacen.almacenDestinoID,
                salidaAlmacen.fechaSalida,
                salidaAlmacen.usuarioID,
                salidaAlmacen.total
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
        public async Task<IActionResult> ActualizarSalidaAlmacen([FromBody] SalidaAlmacen salidaAlmacen)
        {
            if (!ModelState.IsValid || salidaAlmacen == null)
            {
                return BadRequest();
            }

            var respuesta = await _salidasAlmacenRepositorio.EjecutarSpSalidasAlmacen(
                salidaAlmacen.proceso,
                salidaAlmacen.salidaAlmacenID,
                salidaAlmacen.almacenOrigenID,
                salidaAlmacen.almacenDestinoID,
                salidaAlmacen.fechaSalida,
                salidaAlmacen.usuarioID,
                salidaAlmacen.total
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

        [HttpDelete("{salidaAlmacenID:int}")]
        public async Task<IActionResult> EliminarSalidaAlmacen(int salidaAlmacenID)
        {
            var respuesta = await _salidasAlmacenRepositorio.EjecutarSpSalidasAlmacen(
                3,
                salidaAlmacenID,
                0, 0, DateTime.Now, 0, 0
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

