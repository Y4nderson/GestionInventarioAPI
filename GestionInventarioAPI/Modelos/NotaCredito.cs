namespace GestionInventarioAPI.Modelos
{
    public class NotaCredito
    {

        public int proceso { get; set; }
        public int notaCreditoID { get; set; }
        public int compraID { get; set; }
        public string motivo { get; set; }
        public DateTime fechaNota { get; set; }
        public float total { get; set; }
        public int usuarioID { get; set; }

    }
}
