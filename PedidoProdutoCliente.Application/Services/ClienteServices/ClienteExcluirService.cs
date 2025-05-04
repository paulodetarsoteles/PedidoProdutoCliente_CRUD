using PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.ClienteServices
{
    public class ClienteExcluirService(IClienteRepository clienteRepository) : IClienteExcluirService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        public async Task<bool> Process(int id)
        {
            try
            {
                if (id <= 0) return false;

                var cliente = await _clienteRepository.ObterPorId(id);

                if (cliente == null || cliente.DataExclusao != null) return false;

                cliente.DataExclusao = DateTime.Now;

                var result = await _clienteRepository.Excluir(cliente);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
