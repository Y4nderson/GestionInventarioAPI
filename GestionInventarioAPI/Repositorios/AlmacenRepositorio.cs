using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GestionInventarioAPI.Repositorios
{
    public class AlmacenRepositorio
    {


        private readonly AppDbContext _appDbContext;
        public AlmacenRepositorio(AppDbContext appDbContext)
        {
          _appDbContext = appDbContext;
        }


        public async Task<DataSet> EjecutarSpAlmacen(int proceso, int almacenID, string nombreAlmacen, string ubicacion, int usuarioID)
        {

            var procesoParam = new SqlParameter("@PROCESO",SqlDbType.Int) {Value = proceso};
            var almacenIDParam = new SqlParameter("@ALMACENID", SqlDbType.Int) { Value = almacenID };
            var nombreAlmacenParam = new SqlParameter("@NOMBRE_ALMACEN", SqlDbType.VarChar, 100) { Value = nombreAlmacen };
            var ubicacionParam = new SqlParameter("@UBICACION", SqlDbType.VarChar, 255) { Value = ubicacion };
            var usuarioIDParam = new SqlParameter("@USUARIOID", SqlDbType.Int) { Value = usuarioID };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;


            var dateSet = new DataSet();

            using (var comand = _appDbContext.Database.GetDbConnection().CreateCommand())
            {

                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "SP_Almacenes";

                comand.Parameters.Add(procesoParam);
                comand.Parameters.Add(almacenIDParam);
                comand.Parameters.Add(nombreAlmacenParam);
                comand.Parameters.Add(ubicacionParam);
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
