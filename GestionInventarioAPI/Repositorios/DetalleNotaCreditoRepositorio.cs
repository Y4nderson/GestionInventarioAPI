﻿using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace GestionInventarioAPI.Repositorios
{
    public class DetalleNotaCreditoRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public DetalleNotaCreditoRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DataSet> EjecutarSpDetalleNotaCredito(int proceso, int detalleNotaID, int notaCreditoID, int productoID, int cantidad, float precioUnitario, float subtotal, float descuento, float neto, float impuesto, float total)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = proceso };
            var detalleNotaIDParam = new SqlParameter("@DETALLENOTAID", SqlDbType.Int) { Value = detalleNotaID };
            var notaCreditoIDParam = new SqlParameter("@NOTACREDITOID", SqlDbType.Int) { Value = notaCreditoID };
            var productoIDParam = new SqlParameter("@PRODUCTOID", SqlDbType.Int) { Value = productoID };
            var cantidadParam = new SqlParameter("@CANTIDAD", SqlDbType.Int) { Value = cantidad };
            var precioUnitarioParam = new SqlParameter("@PRECIO_UNITARIO", SqlDbType.Float) { Value = precioUnitario };
            var subtotalParam = new SqlParameter("@SUBTOTAL", SqlDbType.Float) { Value = subtotal };
            var descuentoParam = new SqlParameter("@DESCUENTO", SqlDbType.Float) { Value = descuento };
            var netoParam = new SqlParameter("@NETO", SqlDbType.Float) { Value = neto };
            var impuestoParam = new SqlParameter("@IMPUESTO", SqlDbType.Float) { Value = impuesto };
            var totalParam = new SqlParameter("@TOTAL", SqlDbType.Float) { Value = total };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DetalleNotaCredito";

                command.Parameters.Add(procesoParam);
                command.Parameters.Add(detalleNotaIDParam);
                command.Parameters.Add(notaCreditoIDParam);
                command.Parameters.Add(productoIDParam);
                command.Parameters.Add(cantidadParam);
                command.Parameters.Add(precioUnitarioParam);
                command.Parameters.Add(subtotalParam);
                command.Parameters.Add(descuentoParam);
                command.Parameters.Add(netoParam);
                command.Parameters.Add(impuestoParam);
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
