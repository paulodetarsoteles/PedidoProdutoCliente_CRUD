using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;
using System.ComponentModel.DataAnnotations;

namespace PedidoProdutoCliente.Application.Services.ClienteServices
{
    public class ClienteAtualizarService(IClienteRepository clienteRepository) : IClienteAtualizarService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<bool>> Process(ClienteRequest.AtualizarClienteRequest request)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidaParametros(ClienteRequest.AtualizarClienteRequest request)
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

        private async Task<bool> AtualizarEntidade(Cliente cliente, ClienteRequest.AtualizarClienteRequest request)
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

            cliente.DataUltimaAtualizacao = DateTime.Now;

            return await _clienteRepository.Atualizar(cliente);
        }
    }
}
