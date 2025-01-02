using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace GestionInventarioAPI.Repositorios
{
    public class CompraRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public CompraRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DataSet> EjecutarSpComprasInventario(int proceso, int compraID, int ordenPedidoID, int proveedorID, DateTime fechaCompra, float total, int usuarioID, int almacenID)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = proceso };
            var compraIDParam = new SqlParameter("@COMPRAID", SqlDbType.Int) { Value = compraID };
            var ordenPedidoIDParam = new SqlParameter("@ORDENPEDIDOID", SqlDbType.Int) { Value = ordenPedidoID };
            var proveedorIDParam = new SqlParameter("@PROVEEDORID", SqlDbType.Int) { Value = proveedorID };
            var fechaCompraParam = new SqlParameter("@FECHA_COMPRA", SqlDbType.DateTime) { Value = fechaCompra };
            var totalParam = new SqlParameter("@TOTAL", SqlDbType.Float) { Value = total };
            var usuarioIDParam = new SqlParameter("@USUARIOID", SqlDbType.Int) { Value = usuarioID };
            var almacenIDParam = new SqlParameter("@ALMACENID", SqlDbType.Int) { Value = almacenID };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_ComprasInventario";

                command.Parameters.Add(procesoParam);
                command.Parameters.Add(compraIDParam);
                command.Parameters.Add(ordenPedidoIDParam);
                command.Parameters.Add(proveedorIDParam);
                command.Parameters.Add(fechaCompraParam);
                command.Parameters.Add(totalParam);
                command.Parameters.Add(usuarioIDParam);
                command.Parameters.Add(almacenIDParam);
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
