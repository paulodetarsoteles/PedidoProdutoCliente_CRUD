using Microsoft.EntityFrameworkCore;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.Contexts;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;
using PedidoProdutoCliente.Infrastructure.TransactionsInterfaces;

namespace PedidoProdutoCliente.Infrastructure.Repository
{
    public class PedidoRepository(PedidoProdutoClienteContext context, IUnityOfWork unityOfWork) : BaseRepository<Pedido>(context, unityOfWork), IPedidoRepository
    {
        private readonly PedidoProdutoClienteContext _context = context;

        public async override Task<List<Pedido>?> ListarPaginado(int page, int pageSize)
        {
            return await _context.Pedidos
                .Where(c => c.DataExclusao == null)
                .OrderBy(c => c.Id)
                .Include(p => p.Cliente)
                .Include(p => p.Produtos)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Pedido>?> BuscarPorClienteId(int clienteId)
        {
            return await _context.Pedidos
                .Where(p => p.ClienteId == clienteId &&
                       p.DataExclusao == null)
                .Include(p => p.Cliente)
                .Include(p => p.Produtos)
                .ToListAsync();
        }
    }
}
