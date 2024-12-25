using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Tienda.BackEnd.Infrastructure.Data.EntityFramework
{
    public class TiendaDeporteDBContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public TiendaDeporteDBContext()
        {

        }

        public TiendaDeporteDBContext(DbContextOptions<TiendaDeporteDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //var serviceProvider = ((IInfrastructure<IServiceProvider>)this).Instance;
                //var config = this.GetService<IConfigurationRoot>();

                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var servidorbd = Environment.GetEnvironmentVariable("servidorbd");
                var usuariobd = Environment.GetEnvironmentVariable("usuariobd");
                var clavebd = Environment.GetEnvironmentVariable("clavebd");

                // get the configuration from the app settings
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    .Build();


                // define the database to use
                optionsBuilder.UseSqlServer(string.Format(config["DefaultConnection"], servidorbd, usuariobd, clavebd));
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

}