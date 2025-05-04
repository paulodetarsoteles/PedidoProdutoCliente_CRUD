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

        public virtual async Task<List<T>?> ListarPaginado(int page, int pageSize)
        {
            return await _context.Set<T>()
                .Where(c => c.DataExclusao == null)
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<T?> ObterPorId(int id)
        {
            return await _context.Set<T>()
                .Where(e => e.Id == id && e.DataExclusao == null)
                .FirstOrDefaultAsync();
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
