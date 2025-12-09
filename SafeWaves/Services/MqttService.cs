using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; // Para decodificar o payload
using System.Threading;
using System.Threading.Tasks;

namespace MQttIoT.Services
{
    public class MqttService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<MQttIoT.Hubs.AlertaHub> _hubContext;
        private IMqttClient? _mqttClient;
        private readonly List<MQttIoT.Models.SensorData> _sensorHistory = new();
        private readonly object _historyLock = new();

        public MqttService(IConfiguration configuration, IHubContext<MQttIoT.Hubs.AlertaHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Eventos
            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                var sensorData = new MQttIoT.Models.SensorData
                {
                    Timestamp = DateTime.Now,
                    Topic = e.ApplicationMessage.Topic,
                    Payload = payload
                };

                lock (_historyLock)
                {
                    _sensorHistory.Add(sensorData);
                    if (_sensorHistory.Count > 100)
                        _sensorHistory.RemoveAt(0);
                }

                await _hubContext.Clients.All.SendAsync("ReceiveSensorData", sensorData, cancellationToken: stoppingToken);
            };

            var options = new MqttClientOptionsBuilder()
                .WithClientId($"mqttnet-{Guid.NewGuid():N}")
                .WithCleanSession()
                .WithWebSocketServer(o => o.WithUri("wss://broker.hivemq.com:8884/mqtt"))
                .Build();

            var topic = _configuration["MQTT:Topic"] ?? "api/alertas/novo";
            var subscribeOptions = factory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => f.WithTopic(topic))
                .Build();

            _mqttClient.ConnectedAsync += async _ =>
            {
                await _mqttClient!.SubscribeAsync(subscribeOptions, stoppingToken);
            };

            _mqttClient.DisconnectedAsync += async _ =>
            {
                if (stoppingToken.IsCancellationRequested) return;
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    await _mqttClient!.ConnectAsync(options, stoppingToken);
                }
                catch { /* tenta novamente no pr�ximo evento */ }
            };

            await _mqttClient.ConnectAsync(options);
        }

        public List<MQttIoT.Models.SensorData> GetSensorHistory()
        {
            lock (_historyLock)
                return _sensorHistory.ToList();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_mqttClient != null)
            {
                try { await _mqttClient.DisconnectAsync(); } catch { }
            }
            await base.StopAsync(cancellationToken);
        }
    }
}