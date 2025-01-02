using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GestionInventarioAPI.Repositorios
{
    public class ArticuloRepositorio
    {



        private readonly AppDbContext _appDbContext;    
        public ArticuloRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
       }


        public async Task<DataSet> EjecutarSpArticulo(int proceso, int productoID, int categoriaID, int subCategoriaID, string nombre, string descripcion, float precioCompra, float precioVenta, int stock,DateTime fechaVencimiento, string estado)
        {

            var procesoParam = new SqlParameter("@PROCESO",SqlDbType.Int) {Value = proceso};
            var productoIDParam = new SqlParameter("@PRODUCTOID", SqlDbType.Int) { Value = productoID };
            var categoriaIDParam = new SqlParameter("@CATEGORIAID", SqlDbType.Int) { Value = categoriaID };
            var subCategoriaIDParam = new SqlParameter("@SUBCATEGORIAID", SqlDbType.Int) { Value = subCategoriaID };
            var nombreParam = new SqlParameter("@NOMBRE",SqlDbType.VarChar, 100) { Value = nombre};
            var descripcionParam = new SqlParameter("@DESCRIPCION", SqlDbType.VarChar, 255) { Value = descripcion };
            var precioCompraParam = new SqlParameter("@PRECIO_COMPRA", SqlDbType.Float) { Value = precioCompra};
            var precioVentaParam = new SqlParameter("@PRECIO_VENTA", SqlDbType.Float) { Value = precioVenta };
            var stockParam = new SqlParameter("@STOCK", SqlDbType.Int) { Value = stock };
            var fechaVencimientoParam = new SqlParameter("@FECHA_VENCIMIENTO",SqlDbType.DateTime) {Value = fechaVencimiento};
            var estadoParam = new SqlParameter("@ESTADO", SqlDbType.VarChar, 20) { Value = estado };
            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;
            var dataSet = new DataSet();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Articulos";
                command.Parameters.Add(procesoParam);
                command.Parameters.Add(productoIDParam);
                command.Parameters.Add(categoriaIDParam);
                command.Parameters.Add(subCategoriaIDParam);
                command.Parameters.Add(nombreParam);
                command.Parameters.Add(descripcionParam);
                command.Parameters.Add(precioCompraParam);
                command.Parameters.Add(precioVentaParam);
                command.Parameters.Add(stockParam);
                command.Parameters.Add(fechaVencimientoParam);
                command.Parameters.Add(estadoParam);
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
