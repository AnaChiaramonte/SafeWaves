using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SafeWaves.Data;
using SafeWaves.Models;

using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using MQttIoT.Hubs;

namespace SafeWaves.Controllers
{
    public class AlertasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<AlertaHub> _hubContext;
        private readonly ILogger<AlertasController> _logger;

        private static bool mqttInitialized = false;
        private static IMqttClient mqttClient;

        public AlertasController(
            ApplicationDbContext context,
            IHubContext<AlertaHub> hubContext,
            ILogger<AlertasController> logger)
        {
            _context = context;
            _hubContext = hubContext;
            _logger = logger;

            // Inicializa MQTT apenas uma vez por aplicação
            if (!mqttInitialized)
            {
                mqttInitialized = true;
                _logger.LogInformation("Iniciando MQTT no construtor.");
                _ = Task.Run(IniciarMQTT);
            }
        }

        // Página de listagem de alertas
        public async Task<IActionResult> Index()
        {
            var alertas = await _context.Alertas
                .Include(a => a.Usuario)
                .ToListAsync();
            return View(alertas);
        }

        // Inicialização e escuta MQTT
        private async Task IniciarMQTT()
        {
            _logger.LogInformation("Método IniciarMQTT chamado.");
            try
            {
                var factory = new MqttFactory();
                mqttClient = factory.CreateMqttClient();

                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer("broker.hivemq.com", 1883)
                    .WithClientId("ESP32_SafeWaves_" + Guid.NewGuid())
                    .WithCleanSession()
                    .Build();

                mqttClient.ApplicationMessageReceivedAsync += async e =>
                {
                    string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    _logger.LogInformation("MQTT recebido: {Payload}", payload);
                    Console.WriteLine("MQTT recebido: " + payload);

                    try
                    {
                        var alertaMQTT = JsonConvert.DeserializeObject<AlertaMQTT>(payload);

                        if (alertaMQTT == null)
                        {
                            _logger.LogWarning("Payload MQTT inválido ou nulo.");
                            return;
                        }

                        var alerta = new Alerta
                        {
                            DataHora = DateTime.Now,
                            Tipo = alertaMQTT.tipo,
                            Mensagem = alertaMQTT.mensagem,
                            Resolvido = false,
                            UsuarioId = 0
                        };

                        _context.Alertas.Add(alerta);
                        await _context.SaveChangesAsync();

                        await _hubContext.Clients.All.SendAsync("ReceberAlerta", new
                        {
                            dataHora = alerta.DataHora,
                            tipo = alerta.Tipo,
                            mensagem = alerta.Mensagem
                        });

                        _logger.LogInformation("Alerta salvo e enviado via SignalR.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao processar mensagem MQTT.");
                    }
                };

                await mqttClient.ConnectAsync(options);
                await mqttClient.SubscribeAsync("api/alertas/novo");
                _logger.LogInformation("Conectado ao broker MQTT e inscrito no tópico api/alertas/novo.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao iniciar MQTT.");
            }
        }

        // DTO para deserializar o JSON do ESP32
        public class AlertaMQTT
        {
            public string tipo { get; set; }
            public string mensagem { get; set; }
        }
    }
}