using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Infrastructure.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(x => x.Nome).IsRequired().IsUnicode(false).HasMaxLength(150).HasComment("Nome do produto");
            builder.Property(x => x.Valor).IsRequired().HasComment("Valor do produto");
            builder.Property(x => x.Quantidade).IsRequired().HasComment("Qantidade em estoque");
        }
    }
}
