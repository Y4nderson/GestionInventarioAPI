using GestionInventarioAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace GestionInventarioAPI.Repositorios
{
    public class UsuariosRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public UsuariosRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DataSet> EjecutarSpUsuarios(int proceso, int usuarioID, string nombreUsuario, string contrasena, string correoElectronico, string nombreCompleto, int estado, string rol, string permisos, DateTime fechaDeCreacion, DateTime ultimoAcceso)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = proceso };
            var usuarioIDParam = new SqlParameter("@USUARIOID", SqlDbType.Int) { Value = usuarioID };
            var nombreUsuarioParam = new SqlParameter("@NOMBRE_USUARIO", SqlDbType.VarChar, 50) { Value = nombreUsuario };
            var contrasenaParam = new SqlParameter("@CONTRASENA", SqlDbType.VarChar, 255) { Value = contrasena };
            var correoElectronicoParam = new SqlParameter("@CORREO_ELECTRONICO", SqlDbType.VarChar, 100) { Value = correoElectronico };
            var nombreCompletoParam = new SqlParameter("@NOMBRE_COMPLETO", SqlDbType.VarChar, 100) { Value = nombreCompleto };
            var estadoParam = new SqlParameter("@ESTADO", SqlDbType.Int) { Value = estado };
            var rolParam = new SqlParameter("@ROL", SqlDbType.VarChar, 100) { Value = rol };
            var permisosParam = new SqlParameter("@PERMISOS", SqlDbType.VarChar, 100) { Value = permisos };
            var fechaDeCreacionParam = new SqlParameter("@FECHA_DE_CREACION", SqlDbType.DateTime) { Value = fechaDeCreacion };
            var ultimoAccesoParam = new SqlParameter("@ULTIMO_ACCESO", SqlDbType.DateTime) { Value = ultimoAcceso };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 100);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Usuarios";

                command.Parameters.Add(procesoParam);
                command.Parameters.Add(usuarioIDParam);
                command.Parameters.Add(nombreUsuarioParam);
                command.Parameters.Add(contrasenaParam);
                command.Parameters.Add(correoElectronicoParam);
                command.Parameters.Add(nombreCompletoParam);
                command.Parameters.Add(estadoParam);
                command.Parameters.Add(rolParam);
                command.Parameters.Add(permisosParam);
                command.Parameters.Add(fechaDeCreacionParam);
                command.Parameters.Add(ultimoAccesoParam);
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
