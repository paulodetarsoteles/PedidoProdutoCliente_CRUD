using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.ClienteServices
{
    public class ClienteObterPorIdService(IClienteRepository clienteRepository) : IClienteObterPorIdService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        public async Task<BaseResponse<Cliente>> Process(int id)
        {
            try
            {

                if (id <= 0)
                {
                    return new BaseResponse<Cliente>(false, false, ["O Id precisa ser maior que zero"]);
                }

                var result = await _clienteRepository.ObterPorId(id);

                if (result == null)
                {
                    return new BaseResponse<Cliente>(false, "Nenhum cliente encontrado.");
                }

                return new BaseResponse<Cliente>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
