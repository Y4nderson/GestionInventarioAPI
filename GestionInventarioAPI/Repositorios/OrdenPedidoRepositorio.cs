using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace GestionInventarioAPI.Repositorios
{
    public class OrdenPedidoRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public OrdenPedidoRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DataSet> EjecutarSpOrdenPedido(int proceso, int ordenPedidoID, int proveedorID, DateTime fechaOrden, float total, int usuarioID)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = proceso };
            var ordenPedidoIDParam = new SqlParameter("@ORDENPEDIDOID", SqlDbType.Int) { Value = ordenPedidoID };
            var proveedorIDParam = new SqlParameter("@PROVEEDORID", SqlDbType.Int) { Value = proveedorID };
            var fechaOrdenParam = new SqlParameter("@FECHA_ORDEN", SqlDbType.DateTime) { Value = fechaOrden };
            var totalParam = new SqlParameter("@TOTAL", SqlDbType.Float) { Value = total };
            var usuarioIDParam = new SqlParameter("@USUARIO_ID", SqlDbType.Int) { Value = usuarioID };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_OrdenPedido";

                command.Parameters.Add(procesoParam);
                command.Parameters.Add(ordenPedidoIDParam);
                command.Parameters.Add(proveedorIDParam);
                command.Parameters.Add(fechaOrdenParam);
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
