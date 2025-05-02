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

        public async Task<List<Pedido>?> BuscarPorClienteId(int clienteId)
        {
            return await _context.Pedidos
                .Where(p => p.ClienteId == clienteId &&
                       p.DataExclusao == null)
                .ToListAsync();
        }
    }
}
