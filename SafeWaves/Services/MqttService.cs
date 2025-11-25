using MQTTnet;
using MQTTnet.Client;
using System.Text;
using System.Text.Json;
using SafeWaves.Data;
using SafeWaves.Models;

namespace SafeWaves.Services
{
    public class MqttService
    {
        private readonly ApplicationDbContext _context;
        private IMqttClient _client;

        private readonly string _broker = "broker.hivemq.com";
        private readonly int _port = 1883;
        private readonly string _topic = "SafeWavesSenai790/alerta";

        public MqttService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task StartAsync()
        {
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            // CONFIGURAÇÃO (MQTTnet 4.x usa Create options)
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_broker, _port)
                .Build();

            // EVENTO DE MENSAGEM RECEBIDA (4.x usa "ApplicationMessageReceivedAsync")
            _client.ApplicationMessageReceivedAsync += async e =>
            {
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                Console.WriteLine("📩 MQTT RECEBIDO -> " + payload);

                try
                {
                    var dados = JsonSerializer.Deserialize<Movimentacao>(payload);

                    if (dados != null)
                    {
                        _context.Movimentacoes.Add(dados);
                        await _context.SaveChangesAsync();
                        Console.WriteLine("💾 Movimentação salva no banco!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ ERRO MQTT: " + ex.Message);
                }
            };

            // EVENTO DE CONEXÃO (4.x usa ConnectedAsync)
            _client.ApplicationMessageReceivedAsync += async e =>
            {
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                Console.WriteLine("📩 MQTT RECEBIDO -> " + payload);

                try
                {
                    var dados = JsonSerializer.Deserialize<Movimentacao>(payload);

                    if (dados != null)
                    {
                        _context.Movimentacoes.Add(dados);
                        await _context.SaveChangesAsync();
                        Console.WriteLine("💾 Movimentação salva no banco!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ ERRO MQTT: " + ex.Message);
                }
            };

        }
    }
}
