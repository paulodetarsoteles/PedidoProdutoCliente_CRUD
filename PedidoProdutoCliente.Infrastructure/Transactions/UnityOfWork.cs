using Microsoft.EntityFrameworkCore.Storage;
using PedidoProdutoCliente.Infrastructure.Contexts;
using PedidoProdutoCliente.Infrastructure.TransactionsInterfaces;

namespace PedidoProdutoCliente.Infrastructure.Transactions
{
    public class UnityOfWork(PedidoProdutoClienteContext db) : IUnityOfWork
    {
        private readonly PedidoProdutoClienteContext _DbPedidoProdutoCliente = db;
        private IDbContextTransaction? DbContextTransaction { get; set; }

        public async Task IniciarTransacao()
        {
            DbContextTransaction = await _DbPedidoProdutoCliente.Database.BeginTransactionAsync();
        }

        public async Task<bool> Commit()
        {
            var ret = await _DbPedidoProdutoCliente.SaveChangesAsync() > 0;
            DbContextTransaction?.Commit();
            return ret;
        }

        public void Dispose()
        {
            _DbPedidoProdutoCliente?.Dispose();
            DbContextTransaction?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
