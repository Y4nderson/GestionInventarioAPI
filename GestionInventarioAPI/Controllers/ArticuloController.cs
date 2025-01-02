using GestionInventarioAPI.Modelos;
using GestionInventarioAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GestionInventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {

        private readonly ArticuloRepositorio _articuloRepositorio;
        public ArticuloController(ArticuloRepositorio articuloRepositorio)
        {

            _articuloRepositorio = articuloRepositorio;
        }

        [HttpGet]

        public async Task<IActionResult> ObtenerArticulo()
        {

            var respuesta = await _articuloRepositorio.EjecutarSpArticulo(90, 0, 0, 0, "", "", 0, 0, 0, DateTime.Now, "");


            if (respuesta != null && respuesta.Tables.Count > 0)
            {
                var ListaRespuesta = new List<Dictionary<string, object>>();



                foreach (DataRow fila in respuesta.Tables[0].Rows)
                {
                    var filaDatos = new Dictionary<string, object>();

                    foreach (DataColumn columna in respuesta.Tables[0].Columns)
                    {

                        filaDatos[columna.ColumnName] = fila[columna];

                    }
                    ListaRespuesta.Add(filaDatos);

                }
                return Ok(ListaRespuesta);

            }
            else
            {
                return StatusCode(500, "Error del servidor ");
            }

        }


        [HttpPost]
        public async Task<IActionResult> CrearArticulo([FromBody] Articulo articulo)
        {

            if (!ModelState.IsValid || articulo == null)
            {
                return BadRequest();
            }

            var respuesta = await _articuloRepositorio.EjecutarSpArticulo(
                articulo.proceso,
                articulo.productoID,
                articulo.categoriaID,
                articulo.subCategoriaID,
                articulo.nombre,
                articulo.descripcion,
                articulo.precioCompra,
                articulo.precioVenta,
                articulo.stock,
                articulo.fechaVencimiento,
                articulo.estado


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
        public async Task<IActionResult> ActualizarArticulo([FromBody] Articulo articulo)
        {

            if (!ModelState.IsValid || articulo == null)
            {
                return BadRequest();
            }

            var respuesta = await _articuloRepositorio.EjecutarSpArticulo(
                articulo.proceso,
                articulo.productoID,
                articulo.categoriaID,
                articulo.subCategoriaID,
                articulo.nombre,
                articulo.descripcion,
                articulo.precioCompra,
                articulo.precioVenta,
                articulo.stock,
                articulo.fechaVencimiento,
                articulo.estado


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


        [HttpDelete("{productoID:int}")]
        public async Task<IActionResult> EliminarArticulo(int productoID)
        {


            var respuesta = await _articuloRepositorio.EjecutarSpArticulo(
                3,
                productoID,
               0,0,"","",0,0,0,DateTime.Now,""
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
