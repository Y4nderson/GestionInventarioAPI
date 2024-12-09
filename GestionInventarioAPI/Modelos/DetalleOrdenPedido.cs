namespace GestionInventarioAPI.Modelos
{
    public class DetalleOrdenPedido
    {
        public int proceso {  get; set; }
        public int detalleOrdenID { get; set; }
        public int ordenPedidoID { get; set; }
        public int productoID { get; set; }
        public int cantidad { get; set; }
        public int precioUnitario { get; set; }
        public int subtotal { get; set; }
        public int descuento { get; set; }
        public int neto { get; set; }
        public int impuesto { get; set; }
        public int total { get; set; }
   


    }
}
