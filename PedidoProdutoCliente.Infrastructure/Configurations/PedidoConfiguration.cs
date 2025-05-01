using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Infrastructure.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.Property(x => x.PagamentoForma).IsRequired().IsUnicode(false).HasMaxLength(150).HasComment("Forma de pagamento");
            builder.Property(x => x.Parcelas).IsRequired().HasComment("Quantidade de parcelas");
            builder.Property(x => x.ValorParcela).IsRequired().HasComment("Valor da parcela");
            builder.Property(x => x.ValorTotal).IsRequired().HasComment("Valor total do pedido");
        }
    }
}
