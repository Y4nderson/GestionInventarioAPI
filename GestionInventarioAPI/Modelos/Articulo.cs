namespace GestionInventarioAPI.Modelos
{
    public class Articulo
    {

        public int proceso { get; set; }
        public int productoID { get; set; }
        public int categoriaID { get; set; }
        public int subCategoriaID { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public float precioCompra { get; set; }
        public float precioVenta { get; set; }
        public int stock { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string estado { get; set; }

    }
}
