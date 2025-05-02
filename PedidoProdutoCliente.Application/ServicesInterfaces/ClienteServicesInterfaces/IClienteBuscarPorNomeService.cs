using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces
{
    public interface IClienteBuscarPorNomeService
    {
        Task<BaseResponse<List<Cliente>>> Process(string nomeRequest);
    }
}
