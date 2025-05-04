using PedidoProdutoCliente.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace PedidoProdutoCliente.Domain.Models
{
    public class Pedido : IEntity
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public required string PagamentoForma { get; set; }
        public int Parcelas { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Observacoes { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public DateTime? DataExclusao { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }
        public List<Produto>? Produtos { get; set; }
    }
}
