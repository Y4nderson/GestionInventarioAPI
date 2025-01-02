using GestionInventarioAPI.Data;
using GestionInventarioAPI.Modelos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GestionInventarioAPI.Repositorios
{
    public class SubCategoriaRepositorio
    {


        private readonly AppDbContext _appDbContext;
        public SubCategoriaRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        public async Task<DataSet> EjecutarSpSubCategoria(int proceso, int subCategoriaID,int CategoriaID, string nombre, int usuarioID)
        {

            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = proceso };
            var subCategoriaIDParam = new SqlParameter("@SUBCATEGORIAID", SqlDbType.Int) { Value = subCategoriaID };
            var categoriaIDParam = new SqlParameter("@CATEGORIAID", SqlDbType.Int) { Value = CategoriaID };
            var nombreParam = new SqlParameter("@NOMBRE", SqlDbType.VarChar, 50) { Value = nombre };
            var usuarioIDParam = new SqlParameter("@USUARIOID", SqlDbType.Int) { Value = usuarioID };
            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;


            var dateSet = new DataSet();

            using (var comand = _appDbContext.Database.GetDbConnection().CreateCommand())
            {

                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "SP_SubCategorias";

                comand.Parameters.Add(procesoParam);
                comand.Parameters.Add(subCategoriaIDParam);
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
