namespace PedidoProdutoCliente.Infrastructure.RepositoryInterfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> ObterPorId(int id);
        Task<List<T>?> ListarPaginado(int page, int pageSize);
        Task<bool> Adicionar(T entity);
        Task<bool> Atualizar(T entity);
        Task<bool> Excluir(T entity);
    }
}
