namespace PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces
{
    public interface IPedidoExcluirService
    {
        Task<bool> Process(int id);
    }
}
