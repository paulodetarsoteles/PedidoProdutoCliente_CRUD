using PedidoProdutoCliente.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace PedidoProdutoCliente.Domain.Models
{
    public class Produto : IEntity
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public DateTime Datacadastro { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public DateTime? DataExclusao { get; set; }

        [JsonIgnore]
        public List<Pedido>? Pedidos { get; set; }
    }
}
