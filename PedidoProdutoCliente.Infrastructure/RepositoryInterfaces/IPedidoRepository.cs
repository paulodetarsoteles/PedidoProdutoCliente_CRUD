using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Infrastructure.RepositoryInterfaces
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        Task<List<Pedido>?> BuscarPorClienteId(int clienteId);
    }
}
