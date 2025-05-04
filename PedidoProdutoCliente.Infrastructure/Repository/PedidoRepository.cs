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

        public override async Task<Pedido?> ObterPorId(int id)
        {
            return await _context.Pedidos
                .Where(p => p.Id == id)
                .Include(p => p.Cliente)
                .Include(p => p.Produtos)
                .FirstOrDefaultAsync();
        }
    }
}
