using Microsoft.AspNetCore.SignalR;

namespace SafeWaves.Hubs
{
    public class AlertaHub : Hub
    {
        // Método existente para confirmar alertas
        public async Task ConfirmarAlerta(string idAlerta)
        {
            Console.WriteLine($"Alerta {idAlerta} confirmado pelo cliente {Context.ConnectionId}");
            await Clients.All.SendAsync("AlertaConfirmado", idAlerta);
        }

        // NOVO: Método opcional para enviar um alerta manualmente a todos os clientes
        public async Task EnviarAlerta(string mensagem, string tipo)
        {
            var alerta = new
            {
                Mensagem = mensagem,
                Tipo = tipo,
                DataHora = DateTime.Now
            };

            await Clients.All.SendAsync("ReceberAlerta", alerta);
        }
    }
}
