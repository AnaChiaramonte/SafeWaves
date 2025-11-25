namespace SafeWaves.Models
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public double Valor { get; set; }
        public DateTime DataHora { get; set; }
    }
}

