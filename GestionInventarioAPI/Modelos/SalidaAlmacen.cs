namespace GestionInventarioAPI.Modelos
{
    public class SalidaAlmacen
    {


        public int proceso {  get; set; }
        public int salidaAlmacenID { get; set; }
        public int almacenOrigenID { get; set; }
        public int almacenDestinoID { get; set; }
        public DateTime fechaSalida { get; set; }
        public int usuarioID { get; set; }
        public float total { get; set; }

    }
}
