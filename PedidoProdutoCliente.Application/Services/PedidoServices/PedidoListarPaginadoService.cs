using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.PedidoServices
{
    public class PedidoListarPaginadoService(IPedidoRepository pedidoRepository) : IPedidoListarPaginadoService
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<List<Pedido>>> Process(int page, int pageSize)
        {
            try
            {
                if (ValidaParametros(page, pageSize) == false)
                {
                    return new BaseResponse<List<Pedido>>(false, false, notifications);
                }

                var pedidos = await _pedidoRepository.ListarPaginado(page, pageSize);

                if (pedidos == null || pedidos.Count == 0)
                {
                    return new BaseResponse<List<Pedido>>(false, "Nenhum Pedido encontrado.");
                }

                return new BaseResponse<List<Pedido>>(pedidos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidaParametros(int page, int pageSize)
        {
            if (page <= 0)
            {
                notifications.Add("Page não pode ser igual ou menor que zero");
            }

            if (pageSize <= 0)
            {
                notifications.Add("PageSize não pode ser igual ou menor que zero");
            }

            if (notifications.Count != 0)
            {
                return false;
            }

            return true;
        }
    }
}
