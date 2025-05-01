namespace PedidoProdutoCliente.Domain.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public DateTime Datacadastro { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public DateTime? DataExclusao { get; set; }

        public List<Pedido>? Pedidos { get; set; }
    }
}
