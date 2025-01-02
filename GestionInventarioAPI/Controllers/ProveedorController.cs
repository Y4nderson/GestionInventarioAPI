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
    public class ProveedoresController : ControllerBase
    {
        private readonly ProveedoresRepositorio _proveedoresRepositorio;

        public ProveedoresController(ProveedoresRepositorio proveedoresRepositorio)
        {
            _proveedoresRepositorio = proveedoresRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProveedores()
        {
            var respuesta = await _proveedoresRepositorio.EjecutarSpProveedores(90, 0, "", 0, "", "", "", "");

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
        public async Task<IActionResult> CrearProveedor([FromBody] Proveedor proveedor)
        {
            if (!ModelState.IsValid || proveedor == null)
            {
                return BadRequest();
            }

            var respuesta = await _proveedoresRepositorio.EjecutarSpProveedores(
                proveedor.proceso,
                proveedor.proveedorID,
                proveedor.tipoCedula,
                proveedor.cedula,
                proveedor.nombre,
                proveedor.email,
                proveedor.telefono,
                proveedor.direccion
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
        public async Task<IActionResult> ActualizarProveedor([FromBody] Proveedor proveedor)
        {
            if (!ModelState.IsValid || proveedor == null)
            {
                return BadRequest();
            }

            var respuesta = await _proveedoresRepositorio.EjecutarSpProveedores(
                proveedor.proceso,
                proveedor.proveedorID,
                proveedor.tipoCedula,
                proveedor.cedula,
                proveedor.nombre,
                proveedor.email,
                proveedor.telefono,
                proveedor.direccion
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

        [HttpDelete("{proveedorID:int}")]
        public async Task<IActionResult> EliminarProveedor(int proveedorID)
        {
            var respuesta = await _proveedoresRepositorio.EjecutarSpProveedores(
                3,
                proveedorID,
                "", 0, "", "", "", ""
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
