using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Application.ServicesInterfaces
{
    public interface IClienteListarPaginadoService
    {
        Task<BaseResponse<List<Cliente>>> Process(int page, int pageSize);
    }
}
