using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace GestionInventarioAPI.Repositorios
{
    public class ProveedoresRepositorio
    {
        private readonly AppDbContext _appDbContext;
        
        public ProveedoresRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DataSet> EjecutarSpProveedores(int proceso, int proveedorID, string tipoCedula, int cedula, string nombre, string email, string telefono, string direccion)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = proceso };
            var proveedorIDParam = new SqlParameter("@PROVEEDORID", SqlDbType.Int) { Value = proveedorID };
            var tipoCedulaParam = new SqlParameter("@TIPO_CEDULA", SqlDbType.VarChar, 20) { Value = tipoCedula };
            var cedulaParam = new SqlParameter("@CEDULA", SqlDbType.Int) { Value = cedula };
            var nombreParam = new SqlParameter("@NOMBRE", SqlDbType.VarChar, 100) { Value = nombre };
            var emailParam = new SqlParameter("@EMAIL", SqlDbType.VarChar, 100) { Value = email };
            var telefonoParam = new SqlParameter("@TELEFONO", SqlDbType.VarChar, 50) { Value = telefono };
            var direccionParam = new SqlParameter("@DIRECCION", SqlDbType.VarChar, 250) { Value = direccion };
            
            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Proveedores";

                command.Parameters.Add(procesoParam);
                command.Parameters.Add(proveedorIDParam);
                command.Parameters.Add(tipoCedulaParam);
                command.Parameters.Add(cedulaParam);
                command.Parameters.Add(nombreParam);
                command.Parameters.Add(emailParam);
                command.Parameters.Add(telefonoParam);
                command.Parameters.Add(direccionParam);
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
