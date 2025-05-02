using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.ProdutoServices
{
    public class ProdutoAdicionarService(IProdutoRepository produtoRepository) : IProdutoAdicionarService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<bool>> Process(ProdutoRequest.AdicionarProdutoRequest request)
        {
            try
            {
                if (await ValidaParametros(request) == false)
                {
                    return new BaseResponse<bool>(false, false, notifications);
                }

                var result = await AdicionarEntidade(request);

                return new BaseResponse<bool>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> ValidaParametros(ProdutoRequest.AdicionarProdutoRequest request)
        {
            if (request == null)
            {
                notifications.Add("Request inválido");
                return false;
            }

            if (!string.IsNullOrEmpty(request.Nome))
            {
                notifications.Add("Nome obrigatório");
            }

            if (await _produtoRepository.ValidaProdutoCadastrado(request.Nome))
            {
                notifications.Add("Nome obrigatório");
            }

            if (await _produtoRepository.ValidaProdutoCadastrado(request.Nome) == false)
            {
                notifications.Add("Nome do produto já cadastrado");
            }

            if (request.Valor <= 0)
            {
                notifications.Add("Valor precisa ser maior que zero");
            }

            if (request.Quantidade < 0)
            {
                notifications.Add("Quantidade inválida");
            }

            if (notifications.Count > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> AdicionarEntidade(ProdutoRequest.AdicionarProdutoRequest request)
        {
            var produto = new Produto()
            {
                Nome = request.Nome,
                Valor = request.Valor,
                Quantidade = request.Quantidade,
                Datacadastro = DateTime.UtcNow
            };

            return await _produtoRepository.Adicionar(produto);
        }
    }
}
