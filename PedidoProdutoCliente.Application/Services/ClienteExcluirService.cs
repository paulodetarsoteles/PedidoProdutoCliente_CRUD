using PedidoProdutoCliente.Application.ServicesInterfaces;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services
{
    public class ClienteExcluirService(IClienteRepository clienteRepository) : IClienteExcluirService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        public async Task<bool> Process(int id)
        {
            if (id <= 0) return false;

            var cliente = await _clienteRepository.ObterPorId(id);

            if (cliente == null || cliente.DataExclusao != null) return false;

            cliente.DataExclusao = DateTime.UtcNow;

            var result = await _clienteRepository.Excluir(cliente);

            return true;
        }
    }
}
