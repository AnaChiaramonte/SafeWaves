namespace SafeWaves.Models
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public string Nome { get; set; }
        public string Localizacao { get; set; } // Ex: "Sala de estar"
        public string EnderecoMac { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<LeituraSensor> Leituras { get; set; }
    }
}
