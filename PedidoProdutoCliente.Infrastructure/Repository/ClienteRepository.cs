﻿using Microsoft.EntityFrameworkCore;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.Contexts;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;
using PedidoProdutoCliente.Infrastructure.TransactionsInterfaces;

namespace PedidoProdutoCliente.Infrastructure.Repository
{
    public class ClienteRepository(PedidoProdutoClienteContext context, IUnityOfWork unityOfWork) : BaseRepository<Cliente>(context, unityOfWork), IClienteRepository
    {
        private readonly PedidoProdutoClienteContext _context = context;

        public async Task<List<Cliente>?> BuscarPorNome(string nome)
        {
            return await _context.Clientes
                .Where(c => c.Nome.ToLower().Contains(nome.ToLower()) &&
                       c.DataExclusao == null)
                .Include(c => c.Pedidos)
                .ToListAsync();
        }

        public async Task<bool> ValidaCpfCadastrado(string cpf)
        {
            return await _context.Clientes
                .AnyAsync(c => c.CPF == cpf);
        }

        public override async Task<Cliente?> ObterPorId(int id)
        {
            return await _context.Clientes
                .Where(c => c.Id == id)
                .Include(c => c.Pedidos)
                .FirstOrDefaultAsync();
        }
    }
}
