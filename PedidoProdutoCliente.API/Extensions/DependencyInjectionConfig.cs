using PedidoProdutoCliente.Application.Services;
using PedidoProdutoCliente.Application.ServicesInterfaces;
using PedidoProdutoCliente.Infrastructure.Repository;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;
using PedidoProdutoCliente.Infrastructure.Transactions;
using PedidoProdutoCliente.Infrastructure.TransactionsInterfaces;

namespace PedidoProdutoCliente.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteListarPaginadoService, ClienteListarPaginadoService>();
            services.AddScoped<IClienteBuscarPorNomeService, ClienteBuscarPorNomeService>();
            services.AddScoped<IClienteAdicionarService, ClienteAdicionarService>();
            services.AddScoped<IClienteAtualizarService, ClienteAtualizarService>();
            services.AddScoped<IClienteExcluirService, ClienteExcluirService>();
        }
    }
}
