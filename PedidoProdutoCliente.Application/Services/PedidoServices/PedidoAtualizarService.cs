using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.PedidoServices
{
    public class PedidoAtualizarService(IPedidoRepository pedidoRepository,
        IProdutoRepository produtoRepository) : IPedidoAtualizarService
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<bool>> Process(PedidoRequest.AtualizarPedidoRequest request)
        {
            try
            {
                if (ValidaParametros(request) == false)
                {
                    return new BaseResponse<bool>(false, false, notifications);
                }

                var pedido = await _pedidoRepository.ObterPorId(request.Id);

                if (pedido == null)
                {
                    return new BaseResponse<bool>(false, "Nenhum pedido encontrado.");
                }

                var produtos = new List<Produto>();

                if (request.ProdutosId != null && request.ProdutosId.Count > 0)
                {
                    produtos = await BuscarProdutos(request.ProdutosId);

                    if (produtos == null || produtos.Count <= 0)
                    {
                        return new BaseResponse<bool>(false, false, notifications);
                    }

                    pedido.Produtos = produtos;
                }
                else
                {
                    pedido.Produtos = await _produtoRepository.BuscaProdutosPorPedidoId(pedido.Id);
                }

                var result = await AtualizarEntidade(pedido, request);

                return new BaseResponse<bool>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidaParametros(PedidoRequest.AtualizarPedidoRequest request)
        {
            if (request == null)
            {
                notifications.Add("Request inválido.");
                return false;
            }

            if (request.Id <= 0)
            {
                notifications.Add("Id informado é inválido.");
            }

            if (request.Parcelas != null && request.Parcelas <= 0)
            {
                notifications.Add("Quantidade de parcelas deve ser maior que zero");
            }

            if (notifications.Count > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<List<Produto>> BuscarProdutos(List<int> produtosId)
        {
            var produtos = await _produtoRepository.BuscarProdutosPorId(produtosId);

            if (produtos == null || produtos.Count == 0)
            {
                notifications.Add("Um ou mais produtos informados são inválidos");
                return [];
            }

            if (produtos.Any(p => p.Quantidade <= 0))
            {
                notifications.Add("Um ou mais produtos informados não tem em estoque");
                return [];
            }

            return produtos;
        }

        private async Task<bool> AtualizarEntidade(Pedido pedido, PedidoRequest.AtualizarPedidoRequest request)
        {
            if (request.Parcelas != null)
            {
                pedido.Parcelas = (int)request.Parcelas;
            }

            if (string.IsNullOrEmpty(request.Observacoes) == false)
            {
                pedido.Observacoes = request.Observacoes;
            }

            if (string.IsNullOrEmpty(request.PagamentoForma) == false)
            {
                pedido.PagamentoForma = request.PagamentoForma;
            }


            if (pedido.Produtos != null)
            {
                decimal valorTotalPedido = 0.00M;

                foreach (var item in pedido.Produtos)
                {
                    valorTotalPedido += item.Valor;

                    item.Quantidade--;

                    await _produtoRepository.Atualizar(item);
                }

                pedido.ValorTotal = valorTotalPedido;
            }

            pedido.ValorParcela = Math.Round((pedido.ValorTotal / pedido.Parcelas), 2);

            pedido.DataUltimaAtualizacao = DateTime.UtcNow;

            return await _pedidoRepository.Atualizar(pedido);
        }
    }
}
