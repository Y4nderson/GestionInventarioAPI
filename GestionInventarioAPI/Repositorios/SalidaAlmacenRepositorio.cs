using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace GestionInventarioAPI.Repositorios
{
    public class SalidasAlmacenRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public SalidasAlmacenRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DataSet> EjecutarSpSalidasAlmacen(int proceso, int salidaAlmacenID, int almacenOrigenID, int almacenDestinoID, DateTime fechaSalida, int usuarioID, float total)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = proceso };
            var salidaAlmacenIDParam = new SqlParameter("@SALIDAALMACENID", SqlDbType.Int) { Value = salidaAlmacenID };
            var almacenOrigenIDParam = new SqlParameter("@ALMACENORIGENID", SqlDbType.Int) { Value = almacenOrigenID };
            var almacenDestinoIDParam = new SqlParameter("@ALMACENDESTINOID", SqlDbType.Int) { Value = almacenDestinoID };
            var fechaSalidaParam = new SqlParameter("@FECHA_SALIDA", SqlDbType.DateTime) { Value = fechaSalida };
            var usuarioIDParam = new SqlParameter("@USUARIOID", SqlDbType.Int) { Value = usuarioID };
            var totalParam = new SqlParameter("@TOTAL", SqlDbType.Float) { Value = total };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SalidasAlmacen";

                command.Parameters.Add(procesoParam);
                command.Parameters.Add(salidaAlmacenIDParam);
                command.Parameters.Add(almacenOrigenIDParam);
                command.Parameters.Add(almacenDestinoIDParam);
                command.Parameters.Add(fechaSalidaParam);
                command.Parameters.Add(usuarioIDParam);
                command.Parameters.Add(totalParam);
                command.Parameters.Add(respuestaParam);

                using (var dataAdapter = new SqlDataAdapter((SqlCommand)command))
                {
                    await Task.Run(() =>
                    {
                        dataAdapter.Fill(dataSet);
                    });
                }

                return dataSet;
            }
        }
    }
}
