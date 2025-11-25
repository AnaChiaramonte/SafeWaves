using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using SafeWaves.Data;
using SafeWaves.Models;
using SafeWaves.Hubs;

namespace SafeWaves.Controllers
{
    public class AlertasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<AlertaHub> _hubContext;

        public AlertasController(ApplicationDbContext context, IHubContext<AlertaHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: Alertas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Alertas.Include(a => a.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // =====================================
        // Endpoint para receber alertas do ESP32/Wokwi
        // =====================================
        [HttpPost]
        [Route("api/alertas/novo")]
        public async Task<IActionResult> ReceberAlerta([FromBody] AlertaDto alertaDto)
        {
            if (alertaDto == null) return BadRequest();

            // Salvar no banco
            var alerta = new Alerta
            {
                DataHora = DateTime.Now,
                Tipo = "Wokwi",
                Mensagem = alertaDto.Mensagem,
                Resolvido = false,
                UsuarioId = 0
            };
            _context.Alertas.Add(alerta);
            await _context.SaveChangesAsync();

            // Enviar para todos os clientes conectados via SignalR
            await _hubContext.Clients.All.SendAsync("ReceberAlerta", new
            {
                dataHora = alerta.DataHora,
                tipo = alerta.Tipo,
                mensagem = alerta.Mensagem
            });

            return Ok(new { status = "Alerta recebido e enviado!" });
        }
    }

    // DTO para receber dados do ESP32/Wokwi
    public class AlertaDto
    {
        public string Mensagem { get; set; }
    }
}
