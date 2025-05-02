namespace PedidoProdutoCliente.Application.Models.Requests
{
    public class PedidoRequest
    {
        public class AdicionarPedidoRequest
        {
            public required int ClienteId { get; set; }
            public required string PagamentoForma { get; set; }
            public required decimal ValorParcela { get; set; }
            public required int Parcelas { get; set; }
            public string? Observacoes { get; set; }
            public required List<int> ProdutosId { get; set; }
        }

        public class AtualizarPedidoRequest
        {
            public required int Id { get; set; }
            public string? PagamentoForma { get; set; }
            public decimal? ValorParcela { get; set; }
            public int? Parcelas { get; set; }
            public string? Observacoes { get; set; }
            public required List<int> ProdutosId { get; set; }
        }
    }
}
