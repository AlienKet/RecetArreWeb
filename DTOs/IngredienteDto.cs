namespace RecetArreWeb.DTOs
{
    public class IngredienteDto//DTO para mostrar los ingredientes
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string UnidadMedida { get; set; } = default!;
        public string? Descripcion { get; set; } // ? significa que es opcional
        public DateTime CreadoUtc { get; set; }
    }
    public class IngredienteCreacionDto //DTO para crear ingredientes
    {
        public string Nombre { get; set; } = default!;
        public string UnidadMedida { get; set; } = default!;
        public string? Descripcion { get; set; }
    }
    public class IngredienteModificacionDto //DTO para modificar ingredientes
    {
        public string Nombre { get; set; } = default!;
        public string UnidadMedida { get; set; } = default!;
        public string? Descripcion { get; set; }
    }
}
