using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace GestionInventarioAPI.Repositorios
{
    public class NotaCreditoRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public NotaCreditoRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DataSet> EjecutarSpNotaCredito(int proceso, int notaCreditoID, int compraID, string motivo, DateTime fechaNota, float total, int usuarioID)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = proceso };
            var notaCreditoIDParam = new SqlParameter("@NOTACREDITOID", SqlDbType.Int) { Value = notaCreditoID };
            var compraIDParam = new SqlParameter("@COMPRAID", SqlDbType.Int) { Value = compraID };
            var motivoParam = new SqlParameter("@MOTIVO", SqlDbType.VarChar, 255) { Value = motivo };
            var fechaNotaParam = new SqlParameter("@FECHA_NOTA", SqlDbType.DateTime) { Value = fechaNota };
            var totalParam = new SqlParameter("@TOTAL", SqlDbType.Float) { Value = total };
            var usuarioIDParam = new SqlParameter("@USUARIOID", SqlDbType.Int) { Value = usuarioID };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_NotaCredito";

                command.Parameters.Add(procesoParam);
                command.Parameters.Add(notaCreditoIDParam);
                command.Parameters.Add(compraIDParam);
                command.Parameters.Add(motivoParam);
                command.Parameters.Add(fechaNotaParam);
                command.Parameters.Add(totalParam);
                command.Parameters.Add(usuarioIDParam);
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
