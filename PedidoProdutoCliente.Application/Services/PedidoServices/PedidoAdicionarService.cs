using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.PedidoServices
{
    public class PedidoAdicionarService(IPedidoRepository pedidoRepository,
        IClienteRepository clienteRepository,
        IProdutoRepository produtoRepository) : IPedidoAdicionarService
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<bool>> Process(PedidoRequest.AdicionarPedidoRequest request)
        {
            try
            {
                if (await ValidaParametros(request) == false)
                {
                    return new BaseResponse<bool>(false, false, notifications);
                }

                var produtos = await BuscarProdutos(request.ProdutosId);

                if (produtos == null || produtos.Count <= 0)
                {
                    return new BaseResponse<bool>(false, false, notifications);
                }

                var result = await AdicionarEntidade(request, produtos);

                return new BaseResponse<bool>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> ValidaParametros(PedidoRequest.AdicionarPedidoRequest request)
        {
            if (request == null)
            {
                notifications.Add("Request inválido");
                return false;
            }

            if (request.ClienteId <= 0)
            {
                notifications.Add("ClienteId inválido");
            }

            if (string.IsNullOrEmpty(request.PagamentoForma))
            {
                notifications.Add("Forma de pagamento não pode ser vazia");
            }

            if (request.Parcelas <= 0)
            {
                notifications.Add("Quantidade de parcelas deve ser maior que zero");
            }

            if (request.ProdutosId == null || request.ProdutosId.Count <= 0)
            {
                notifications.Add("Pedido precisa conter pelo menos um número de ProdutoId");
            }

            if (request.ProdutosId != null && request.ProdutosId.Count > 0 && request.ProdutosId.Any(p => p <= 0))
            {
                notifications.Add("Nenhum ProdutoId pode ser igual ou menor que zero");
            }

            if (notifications.Count > 0) return false;

            var cliente = await _clienteRepository.ObterPorId(request.ClienteId);

            if (cliente == null || cliente.DataExclusao != null)
            {
                notifications.Add("Cliente inválido");
            }

            if (notifications.Count > 0) return false;

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

        private async Task<bool> AdicionarEntidade(PedidoRequest.AdicionarPedidoRequest request, List<Produto> produtos)
        {
            decimal valorTotalPedido = 0.00M;

            foreach (var item in produtos)
            {
                valorTotalPedido += item.Valor;

                item.Quantidade--;

                await _produtoRepository.Atualizar(item);
            }

            decimal valorParcela = Math.Round((valorTotalPedido / request.Parcelas), 2);

            var pedido = new Pedido()
            {
                ClienteId = request.ClienteId,
                PagamentoForma = request.PagamentoForma,
                ValorParcela = valorParcela,
                Parcelas = request.Parcelas,
                ValorTotal = valorTotalPedido,
                Observacoes = request.Observacoes,
                Produtos = produtos,
                DataPedido = DateTime.Now
            };

            return await _pedidoRepository.Adicionar(pedido);
        }
    }
}
