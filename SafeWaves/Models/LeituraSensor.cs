namespace SafeWaves.Models
{
    public class LeituraSensor
    {
        public int LeituraSensorId { get; set; }
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }
        public DateTime DataHora { get; set; }
        public double Valor { get; set; } // Exemplo: nível de movimento detectado
    }
}