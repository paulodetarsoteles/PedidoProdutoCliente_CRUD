namespace PedidoProdutoCliente.Infrastructure.TransactionsInterfaces
{
    public interface IUnityOfWork : IDisposable
    {
        Task IniciarTransacao();
        Task<bool> Commit();
    }
}
