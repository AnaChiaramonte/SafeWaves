using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using SafeWaves.Data;
using SafeWaves.Models;
using SafeWaves.Hubs;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;

namespace SafeWaves.Controllers
{
    public class AlertasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<AlertaHub> _hubContext;

        private static bool mqttInitialized = false;
        private static IMqttClient mqttClient;

        public AlertasController(ApplicationDbContext context, IHubContext<AlertaHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;

            // Evitar inicializar MQTT várias vezes
            if (!mqttInitialized)
            {
                mqttInitialized = true;
                _ = Task.Run(() => IniciarMQTT());
            }
        }

        // ================================
        // GET: Alertas (Página)
        // ================================
        public async Task<IActionResult> Index()
        {
            var alertas = _context.Alertas.Include(a => a.Usuario);
            return View(await alertas.ToListAsync());
        }

        // ================================
        // MQTT → Recebe alertas do ESP32
        // ================================
        private async Task IniciarMQTT()
        {
            try
            {
                var factory = new MqttFactory();
                mqttClient = factory.CreateMqttClient();

                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer("broker.hivemq.com", 1883)
                    .WithClientId("SafeWavesServidor")
                    .WithCleanSession()
                    .Build();

                mqttClient.ApplicationMessageReceivedAsync += async e =>
                {
                    try
                    {
                        string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                        Console.WriteLine("MQTT recebido → " + payload);

                        // O ESP32 envia JSON
                        var alertaMQTT = JsonConvert.DeserializeObject<AlertaMQTT>(payload);

                        if (alertaMQTT == null)
                            return;

                        // SALVAR NO BANCO
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

                        // ENVIAR PARA OS CLIENTES VIA SIGNALR
                        await _hubContext.Clients.All.SendAsync("ReceberAlerta", new
                        {
                            dataHora = alerta.DataHora,
                            tipo = alerta.Tipo,
                            mensagem = alerta.Mensagem
                        });

                        Console.WriteLine("Alerta salvo + enviado via SignalR");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao processar MQTT: " + ex.Message);
                    }
                };

                await mqttClient.ConnectAsync(options);
                await mqttClient.SubscribeAsync("SafeWavesSenai790/alerta");

                Console.WriteLine("Servidor conectado ao MQTT e ouvindo alertas...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro MQTT: " + ex.Message);
            }
        }
    }

    // Mapeia o JSON enviado pelo ESP32:
    public class AlertaMQTT
    {
        public string tipo { get; set; }
        public string mensagem { get; set; }
    }
}
