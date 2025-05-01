using Microsoft.EntityFrameworkCore;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.Configurations;

namespace PedidoProdutoCliente.Infrastructure.Contexts
{
    public class PedidoProdutoClienteContext : DbContext
    {
        public PedidoProdutoClienteContext() { }

        public PedidoProdutoClienteContext(DbContextOptions<PedidoProdutoClienteContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=bd_pedido_produto_cliente;User Id=postgres;Password=admin@123;",
                                             p => { p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery); })
                                  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na conexão com o banco de dados. Detalhes: {ex.Message}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
