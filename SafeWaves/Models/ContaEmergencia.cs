namespace SafeWaves.Models
{
    public class ContatoEmergencia
    {
        public int ContatoEmergenciaId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Relacao { get; set; } // Ex: filho, vizinho, cuidador
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}