using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Infrastructure.RepositoryInterfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<List<Produto>?> BuscarPorNome(string nome);
        Task<bool> ValidaProdutoCadastrado(string nome);
    }
}
