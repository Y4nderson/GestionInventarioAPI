using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GestionInventarioAPI.Repositorios
{
    public class CategoriaRepositorio
    {

        private readonly AppDbContext _appDbContext;
        public CategoriaRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<DataSet> EjecutarSpCategoria(int proceso, int categoriaID, string nombre, int usuarioID)
        {

            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = proceso };
            var categoriaIDParam = new SqlParameter("@CATEGORIAID", SqlDbType.Int) { Value = categoriaID };
            var nombreParam = new SqlParameter("@NOMBRE", SqlDbType.VarChar, 50) { Value = nombre };
            var usuarioIDParam = new SqlParameter("@USUARIOID", SqlDbType.Int) { Value = usuarioID };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;


            var dateSet = new DataSet();

            using (var comand = _appDbContext.Database.GetDbConnection().CreateCommand())
            {

                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "SP_Categorias";

                comand.Parameters.Add(procesoParam);
                comand.Parameters.Add(categoriaIDParam);
                comand.Parameters.Add(nombreParam);
                comand.Parameters.Add(usuarioIDParam);
                comand.Parameters.Add(respuestaParam);




                using (var dataAdapter = new SqlDataAdapter((SqlCommand)comand))
                {

                    await Task.Run(() =>
                    {

                        dataAdapter.Fill(dateSet);

                    });

                }
                return dateSet;

            }

        }
    }
}
