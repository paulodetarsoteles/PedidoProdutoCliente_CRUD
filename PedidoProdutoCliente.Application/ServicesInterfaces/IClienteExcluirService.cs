namespace PedidoProdutoCliente.Application.ServicesInterfaces
{
    public interface IClienteExcluirService
    {
        Task<bool> Process(int id);
    }
}
