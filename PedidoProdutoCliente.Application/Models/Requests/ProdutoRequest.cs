namespace PedidoProdutoCliente.Application.Models.Requests
{
    public class ProdutoRequest
    {
        public class AdicionarProdutoRequest
        {
            public required string Nome { get; set; }
            public string? Descricao { get; set; }
            public required decimal Valor { get; set; }
            public required int Quantidade { get; set; }
        }

        public class AtualizarProdutoRequest
        {
            public required int Id { get; set; }
            public string? Nome { get; set; }
            public decimal? Valor { get; set; }
            public int? Quantidade { get; set; }
        }
    }
}
