namespace GestionInventarioAPI.Modelos
{
    public class Usuario
    {
        public int proceso { get; set; }
     
        public int usuarioID { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasenaHash { get; set; }
        public string correoElectronico { get; set; }
        public string nombreCompleto { get; set; }
        public int estado { get; set; }
        public string rol { get; set; }
        public string permisos { get; set; }
        public DateTime fechaDeCreacion { get; set; }
        public DateTime ultimoAcceso { get; set; }

    }
}
