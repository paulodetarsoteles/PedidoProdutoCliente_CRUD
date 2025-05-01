using Microsoft.EntityFrameworkCore.Storage;
using PedidoProdutoCliente.Infrastructure.Contexts;
using PedidoProdutoCliente.Infrastructure.TransactionsInterfaces;

namespace PedidoProdutoCliente.Infrastructure.Transactions
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly PedidoProdutoClienteContext _DbPedidoProdutoCliente;
        private IDbContextTransaction dbContextTransaction { get; set; }

        public UnityOfWork(PedidoProdutoClienteContext db)
        {
            _DbPedidoProdutoCliente = db;
        }

        public async Task IniciarTransacao()
        {
            dbContextTransaction = await _DbPedidoProdutoCliente.Database.BeginTransactionAsync();
        }

        public async Task<bool> Commit()
        {
            var ret = await _DbPedidoProdutoCliente.SaveChangesAsync() > 0;
            dbContextTransaction?.Commit();
            return ret;
        }

        public void Dispose()
        {
            _DbPedidoProdutoCliente?.Dispose();
        }
    }
}
