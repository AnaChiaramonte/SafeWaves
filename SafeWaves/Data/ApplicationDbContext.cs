using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SafeWaves.Models;

namespace SafeWaves.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<LeituraSensor> LeituraSensores { get; set; }
        public DbSet<ZonaSegura> ZonasSeguras { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<ContatoEmergencia> ContatosEmergencias { get; set; }
        public object Leitura { get; internal set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }


    }
}

