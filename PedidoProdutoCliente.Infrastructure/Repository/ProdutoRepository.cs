using Microsoft.EntityFrameworkCore;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.Contexts;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;
using PedidoProdutoCliente.Infrastructure.TransactionsInterfaces;

namespace PedidoProdutoCliente.Infrastructure.Repository
{
    public class ProdutoRepository(PedidoProdutoClienteContext context, IUnityOfWork unityOfWork) : BaseRepository<Produto>(context, unityOfWork), IProdutoRepository
    {
        private readonly PedidoProdutoClienteContext _context = context;

        public async Task<List<Produto>?> BuscarPorNome(string nome)
        {
            return await _context.Produtos
                .Where(p => p.Nome.Contains(nome))
                .ToListAsync();
        }

        public async Task<bool> ValidaProdutoCadastrado(string nome)
        {
            return await _context.Produtos
                .AnyAsync(p => p.Nome.ToUpper() == nome.ToUpper());
        }
    }
}
