using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Infrastructure.RepositoryInterfaces
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<List<Cliente>?> BuscarPorNome(string nome);
    }
}
