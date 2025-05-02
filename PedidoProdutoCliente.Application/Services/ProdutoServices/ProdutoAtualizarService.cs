using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoProduto.Application.Services.ProdutoServices
{
    public class ProdutoAtualizarService(IProdutoRepository produtoRepository) : IProdutoAtualizarService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<bool>> Process(ProdutoRequest.AtualizarProdutoRequest request)
        {
            try
            {
                if (await ValidaParametros(request) == false)
                {
                    return new BaseResponse<bool>(false, false, notifications);
                }

                var produto = await _produtoRepository.ObterPorId(request.Id);

                if (produto == null)
                {
                    return new BaseResponse<bool>(false, "Nenhum produto encontrado.");
                }

                var result = await AtualizarEntidade(produto, request);

                return new BaseResponse<bool>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> ValidaParametros(ProdutoRequest.AtualizarProdutoRequest request)
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

            if (request.Nome != null && await _produtoRepository.ValidaProdutoCadastrado(request.Nome) == false)
            {
                notifications.Add("Nome já cadastrado");
            }

            if (notifications.Count > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> AtualizarEntidade(Produto produto, ProdutoRequest.AtualizarProdutoRequest request)
        {
            if (!string.IsNullOrEmpty(request.Nome))
            {
                produto.Nome = request.Nome;
            }

            if (request.Valor != null)
            {
                produto.Valor = (decimal)request.Valor;
            }

            if (request.Quantidade != null)
            {
                produto.Quantidade = (int)request.Quantidade;
            }

            produto.DataUltimaAtualizacao = DateTime.UtcNow;

            return await _produtoRepository.Atualizar(produto);
        }
    }
}
