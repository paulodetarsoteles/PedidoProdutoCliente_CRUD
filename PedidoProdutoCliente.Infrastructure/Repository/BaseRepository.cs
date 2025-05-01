using Microsoft.EntityFrameworkCore;
using PedidoProdutoCliente.Domain.Interfaces;
using PedidoProdutoCliente.Infrastructure.Contexts;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;
using PedidoProdutoCliente.Infrastructure.TransactionsInterfaces;

namespace PedidoProdutoCliente.Infrastructure.Repository
{
    public class BaseRepository<T>(PedidoProdutoClienteContext context, IUnityOfWork unityOfWork) : IBaseRepository<T> where T : class, IEntity
    {
        private readonly PedidoProdutoClienteContext _context = context;
        private readonly IUnityOfWork _unityOfWork = unityOfWork;

        public async Task<List<T>?> ListarPaginado(int page, int pageSize)
        {
            return await _context.Set<T>()
                                .OrderBy(c => c.Id)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        }

        public async Task<T?> ObterPorId(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Adicionar(T entity)
        {
            await _unityOfWork.IniciarTransacao();

            await _context.AddAsync(entity);

            var result = await _unityOfWork.Commit();

            return result;
        }

        public async Task<bool> Atualizar(T entity)
        {
            await _unityOfWork.IniciarTransacao();

            _context.Update(entity);

            var result = await _unityOfWork.Commit();

            return result;
        }

        public async Task<bool> Excluir(T entity)
        {
            await _unityOfWork.IniciarTransacao();

            _context.Update(entity);

            var result = await _unityOfWork.Commit();

            return result;
        }
    }
}
