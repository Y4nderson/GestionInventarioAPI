namespace GestionInventarioAPI.Modelos
{
    public class DetalleSalida
    {

        public int proceso {  get; set; }
        public int detalleSalidaID { get; set; }
        public int salidaAlmacenID { get; set; }
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
