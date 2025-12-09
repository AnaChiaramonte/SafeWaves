namespace SafeWaves.Models
{
    public class Movimentacao
    {
        public int MovimentacaoId { get; set; }
        public int SensorId { get; set; }
        public double Valor { get; set; }
        public DateTime DataHora { get; set; }
    }
}

