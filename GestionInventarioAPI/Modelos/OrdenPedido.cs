namespace GestionInventarioAPI.Modelos
{
    public class OrdenPedido
    {

        public int proceso { get; set; }
        public int ordenPedidoID { get; set; }
        public int proveedorID { get; set; }
        public int fechaOrden { get; set; }
        public float total { get; set; }
        public int usuarioID { get; set; }


    }
}
