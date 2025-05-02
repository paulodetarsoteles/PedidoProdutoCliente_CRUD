namespace PedidoProdutoCliente.Application.Models.Requests
{
    public class ClienteRequest
    {
        public class Adicionar
        {
            public required string Nome { get; set; }
            public required string CPF { get; set; } = string.Empty;
            public required string Email { get; set; } = string.Empty;
            public string? Telefone { get; set; }
            public string? Endereco { get; set; }
        }

        public class Atualizar
        {
            public required int Id { get; set; }
            public string? Email { get; set; }
            public string? Telefone { get; set; }
            public string? Endereco { get; set; }
        }
    }
}
