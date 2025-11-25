namespace SafeWaves.Models
{
    public class ZonaSegura
    {
        public int Id { get; set; }
        public string Nome { get; set; } // Ex: "Quarto", "Cozinha"
        public string Descricao { get; set; }
        public bool EZonaDeRisco { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}