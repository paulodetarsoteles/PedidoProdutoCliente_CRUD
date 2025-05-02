using PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoproduto.Application.Services.produtoServices
{
    public class ProdutoExcluirService(IProdutoRepository produtoRepository) : IProdutoExcluirService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<bool> Process(int id)
        {
            try
            {
                if (id <= 0) return false;

                var produto = await _produtoRepository.ObterPorId(id);

                if (produto == null || produto.DataExclusao != null) return false;

                produto.DataExclusao = DateTime.UtcNow;

                var result = await _produtoRepository.Excluir(produto);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
