namespace PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces
{
    public interface IClienteExcluirService
    {
        Task<bool> Process(int id);
    }
}
