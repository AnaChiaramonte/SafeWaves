namespace SafeWaves.Models
{
    public class Alerta
    {
        public int AlertaId { get; set; }
        public DateTime? DataHora { get; set; }
        public string Tipo { get; set; } // Ex: "Queda", "Ausência Prolongada"
        public string Mensagem { get; set; }
        public bool? Resolvido { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}