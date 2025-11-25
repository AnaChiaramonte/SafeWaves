namespace SafeWaves.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string Telefone { get; set; }
        public TipoUsuario Tipo { get; set; } // Idoso, Cuidador, Responsável, Administrador
        public ICollection<Sensor> Sensores { get; set; }
        public ICollection<ZonaSegura> ZonasSeguras { get; set; }
    }
    public enum TipoUsuario { Idoso, Cuidador, Responsavel, Administrador }
}
