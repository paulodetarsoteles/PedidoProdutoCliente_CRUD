using PedidoProdutoCliente.Domain.Interfaces;

namespace PedidoProdutoCliente.Domain.Models
{
    public class Cliente : IEntity
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public DateTime? DataExclusao { get; set; }

        public List<Pedido>? Pedidos { get; set; }
    }
}
