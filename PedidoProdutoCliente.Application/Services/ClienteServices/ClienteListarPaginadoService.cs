using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.ClienteServices
{
    public class ClienteListarPaginadoService(IClienteRepository clienteRepository) : IClienteListarPaginadoService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<List<Cliente>>> Process(int page, int pageSize)
        {
            try
            {
                if (ValidaParametros(page, pageSize) == false)
                {
                    return new BaseResponse<List<Cliente>>(false, false, notifications);
                }

                var clientes = await _clienteRepository.ListarPaginado(page, pageSize);

                if (clientes == null || clientes.Count == 0)
                {
                    return new BaseResponse<List<Cliente>>(false, "Nenhum cliente encontrado.");
                }

                return new BaseResponse<List<Cliente>>(clientes);
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
