namespace GestionInventarioAPI.Modelos
{
    public class DetalleCompra
    {

        public int proceso {  get; set; }

        public int detalleCompraID { get; set; }
        public int compraID { get; set; }
        public int productoID { get; set; }
        public int cantidad { get; set; }
        public float precioUnitario { get; set; }
        public float subtotal { get; set; }
        public float descuento { get; set; }
        public float neto { get; set; }
        public float impuesto { get; set; }
        public float total { get; set; }

    }
}
