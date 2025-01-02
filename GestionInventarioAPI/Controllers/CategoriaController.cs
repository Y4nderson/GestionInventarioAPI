using GestionInventarioAPI.Modelos;
using GestionInventarioAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GestionInventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {


        private readonly CategoriaRepositorio _categoriaRepositorio;
        public CategoriaController(CategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategoria()
        {

            var respuesta = await _categoriaRepositorio.EjecutarSpCategoria(90,0,"",0);

            if (respuesta != null && respuesta.Tables.Count > 0)
            {

                var ListaResultado = new List<Dictionary<string, object>>();

                foreach (DataRow fila in respuesta.Tables[0].Rows)
                {

                    var filaDatos = new Dictionary<string, object>();

                    foreach (DataColumn column in respuesta.Tables[0].Columns)
                    {

                        filaDatos[column.ColumnName] = fila[column];

                    }

                    ListaResultado.Add(filaDatos);
                }

                return Ok(ListaResultado);

            }
            else
            {
                return NotFound("No se encontraron datos"); ;
            }

        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria([FromBody] Categoria categoria)
        {

            if (!ModelState.IsValid || categoria == null)
            {
                return BadRequest();
            }

            var respuesta = await _categoriaRepositorio.EjecutarSpCategoria(
                categoria.proceso,
                categoria.categoriaID,
                categoria.nombre,
                categoria.usuarioID



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
        public async Task<IActionResult> ActualizarCategoria([FromBody] Categoria categoria)
        {

            if (!ModelState.IsValid || categoria == null)
            {
                return BadRequest();
            }

            var respuesta = await _categoriaRepositorio.EjecutarSpCategoria(
                categoria.proceso,
                categoria.categoriaID,
                categoria.nombre,
                categoria.usuarioID



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


        [HttpDelete("{categoriaID:int}")]
        public async Task<IActionResult> EliminarCategoria(int categoriaID)
        {


            var respuesta = await _categoriaRepositorio.EjecutarSpCategoria(
                3,
                0, "", 0

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
