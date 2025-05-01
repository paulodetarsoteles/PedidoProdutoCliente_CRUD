using Microsoft.EntityFrameworkCore;

namespace PedidoProdutoCliente.Infrastructure.Contexts
{
    public class PedidoProdutoClienteContext : DbContext
    {
        public PedidoProdutoClienteContext() { }

        public PedidoProdutoClienteContext(DbContextOptions<PedidoProdutoClienteContext> options) : base(options) { }

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
