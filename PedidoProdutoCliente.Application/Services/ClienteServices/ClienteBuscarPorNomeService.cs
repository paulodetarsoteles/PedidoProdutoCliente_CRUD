using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.ClienteServices
{
    public class ClienteBuscarPorNomeService(IClienteRepository clienteRepository) : IClienteBuscarPorNomeService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        public async Task<BaseResponse<List<Cliente>>> Process(string nomeRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeRequest))
                {
                    return new BaseResponse<List<Cliente>>(false, false, ["Nome inválido ou em branco"]);
                }

                var clientes = await _clienteRepository.BuscarPorNome(nomeRequest);

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
    }
}
