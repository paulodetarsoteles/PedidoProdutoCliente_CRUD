using PedidoProdutoCliente.Application.Models.Responses;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces
{
    public interface IProdutoExcluirService
    {
        Task<bool> Process(int id);
    }
}
