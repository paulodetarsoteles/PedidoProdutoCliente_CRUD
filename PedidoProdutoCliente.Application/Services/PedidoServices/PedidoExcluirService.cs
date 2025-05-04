using PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.PedidoServices
{
    public class PedidoExcluirService(IPedidoRepository pedidoRepository) : IPedidoExcluirService
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        public async Task<bool> Process(int id)
        {
            try
            {
                if (id <= 0) return false;

                var produto = await _pedidoRepository.ObterPorId(id);

                if (produto == null || produto.DataExclusao != null) return false;

                produto.DataExclusao = DateTime.Now;

                var result = await _pedidoRepository.Excluir(produto);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
