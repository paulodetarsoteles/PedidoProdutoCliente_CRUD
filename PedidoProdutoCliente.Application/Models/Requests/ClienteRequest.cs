namespace PedidoProdutoCliente.Application.Models.Requests
{
    public class ClienteRequest
    {
        public class AdicionarClienteRequest
        {
            public required string Nome { get; set; }
            public required string CPF { get; set; } = string.Empty;
            public required string Email { get; set; } = string.Empty;
            public string? Telefone { get; set; }
            public string? Endereco { get; set; }
        }

        public class AtualizarClienteRequest
        {
            public required int Id { get; set; }
            public string? Email { get; set; }
            public string? Telefone { get; set; }
            public string? Endereco { get; set; }
        }
    }
}
