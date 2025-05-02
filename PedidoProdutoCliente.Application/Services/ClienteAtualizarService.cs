using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;
using System.ComponentModel.DataAnnotations;

namespace PedidoProdutoCliente.Application.Services
{
    public class ClienteAtualizarService(IClienteRepository clienteRepository) : IClienteAtualizarService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<bool>> Process(ClienteRequest.Atualizar request)
        {
            if (ValidaParametros(request) == false)
            {
                return new BaseResponse<bool>(false, false, notifications);
            }

            var cliente = await _clienteRepository.ObterPorId(request.Id);

            if (cliente == null)
            {
                return new BaseResponse<bool>(false, "Nenhum cliente encontrado.");
            }

            var result = await AtualizarEntidade(cliente, request);

            return new BaseResponse<bool>(result);
        }

        private bool ValidaParametros(ClienteRequest.Atualizar request)
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

            var emailAddressAttribute = new EmailAddressAttribute();

            if (request.Email != null && emailAddressAttribute.IsValid(request.Email) == false)
            {
                notifications.Add("E-mail inválido");
            }

            if (!string.IsNullOrEmpty(request.Telefone) && request.Telefone.Length > 13)
            {
                notifications.Add("Telefone inválido");
            }

            if (notifications.Count > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> AtualizarEntidade(Cliente cliente, ClienteRequest.Atualizar request)
        {
            if (!string.IsNullOrEmpty(request.Email))
            {
                cliente.Email = request.Email;
            }

            if (request.Telefone != null)
            {
                cliente.Telefone = request.Telefone;
            }

            if (request.Endereco != null)
            {
                cliente.Endereco = request.Endereco;
            }

            cliente.DataUltimaAtualizacao = DateTime.UtcNow;

            return await _clienteRepository.Atualizar(cliente);
        }
    }
}
