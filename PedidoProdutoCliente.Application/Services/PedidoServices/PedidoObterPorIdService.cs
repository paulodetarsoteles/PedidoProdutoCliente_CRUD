using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.PedidoServices
{
    public class PedidoObterPorIdService(IPedidoRepository pedidoRepository) : IPedidoObterPorIdService
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        public async Task<BaseResponse<Pedido>> Process(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new BaseResponse<Pedido>(false, false, ["O Id precisa ser maior que zero"]);
                }

                var pedido = await _pedidoRepository.ObterPorId(id);

                if (pedido == null)
                {
                    return new BaseResponse<Pedido>(false, "Nenhum Pedido encontrado.");
                }

                return new BaseResponse<Pedido>(pedido);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
