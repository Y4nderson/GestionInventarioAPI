namespace GestionInventarioAPI.Modelos
{
    public class Compra
    {


        public int proceso {  get; set; }
        public int compraID { get; set; }
        public int ordenPedidoID { get; set; }
        public int proveedorID { get; set; }
        public DateTime fechaCompra { get; set; }
        public float total { get; set; }
        public int usuarioID { get; set; }
        public int almacenID { get; set; }

    }
}
