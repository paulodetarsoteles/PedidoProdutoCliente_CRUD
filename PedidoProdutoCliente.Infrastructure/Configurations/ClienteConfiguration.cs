using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Infrastructure.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Property(x => x.Nome).IsRequired().IsUnicode(false).HasMaxLength(150).HasComment("Nome do cliente");
            builder.Property(x => x.CPF).IsRequired().IsUnicode(false).HasMaxLength(14).HasComment("Documento do cliente (CPF)");
            builder.Property(x => x.Email).IsUnicode(false).HasMaxLength(200).HasComment("Email do cliente");
            builder.Property(x => x.Telefone).IsUnicode(false).HasMaxLength(13).HasComment("Número do telefone do cliente");
        }
    }
}
