﻿using PedidoProdutoCliente.Application.Extensions;
using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.ClienteServices
{
    public class ClienteAdicionarService(IClienteRepository clienteRepository) : IClienteAdicionarService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<bool>> Process(ClienteRequest.AdicionarClienteRequest request)
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

        private async Task<bool> ValidaParametros(ClienteRequest.AdicionarClienteRequest request)
        {
            if (request == null)
            {
                notifications.Add("Request inválido");
                return false;
            }

            if (request.Email == null || Utils.ValidaEmail(request.Email) == false)
            {
                notifications.Add("E-mail inválido");
            }

            if (!string.IsNullOrEmpty(request.Telefone) && request.Telefone.Length > 13)
            {
                notifications.Add("Telefone inválido");
            }

            if (string.IsNullOrEmpty(request.CPF) || Utils.ValidaCPF(request.CPF) == false)
            {
                notifications.Add("CPF inválido");
            }

            if (await _clienteRepository.ValidaCpfCadastrado(request.CPF))
            {
                notifications.Add("CPF já cadastrado");
            }

            if (notifications.Count > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> AdicionarEntidade(ClienteRequest.AdicionarClienteRequest request)
        {
            var cliente = new Cliente()
            {
                Nome = request.Nome,
                CPF = request.CPF,
                Email = request.Email,
                Telefone = request.Telefone,
                Endereco = request.Endereco,
                DataCadastro = DateTime.Now
            };

            return await _clienteRepository.Adicionar(cliente);
        }
    }
}
