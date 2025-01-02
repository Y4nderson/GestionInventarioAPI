using GestionInventarioAPI.Modelos;
using GestionInventarioAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GestionInventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriaController : ControllerBase
    {

        private readonly SubCategoriaRepositorio _subCategoriaRepositorio;

        public SubCategoriaController(SubCategoriaRepositorio subCategoriaRepositorio)
        {
            _subCategoriaRepositorio = subCategoriaRepositorio;
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerSubCategoria()
        {

            var respuesta = await _subCategoriaRepositorio.EjecutarSpSubCategoria(90, 0,0, "", 0);

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
        public async Task<IActionResult> CrearSubCategoria([FromBody] SubCategoria subCategoria)
        {

            if (!ModelState.IsValid || subCategoria == null)
            {
                return BadRequest();
            }

            var respuesta = await _subCategoriaRepositorio.EjecutarSpSubCategoria(
                subCategoria.proceso,
                subCategoria.subCategoriaID,
                subCategoria.categoriaID,
                subCategoria.nombre,
                subCategoria.usuarioID



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
        public async Task<IActionResult> ActualizarSubCategoria([FromBody] SubCategoria subCategoria)
        {

            if (!ModelState.IsValid || subCategoria == null)
            {
                return BadRequest();
            }

            var respuesta = await _subCategoriaRepositorio.EjecutarSpSubCategoria(
                subCategoria.proceso,
                subCategoria.subCategoriaID,
                subCategoria.categoriaID,
                subCategoria.nombre,
                subCategoria.usuarioID



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


        [HttpDelete("{SubcategoriaID:int}")]
        public async Task<IActionResult> EliminarSubCategoria(int SubcategoriaID)
        {


            var respuesta = await _subCategoriaRepositorio.EjecutarSpSubCategoria(3, 0, 0, "", 0);



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
