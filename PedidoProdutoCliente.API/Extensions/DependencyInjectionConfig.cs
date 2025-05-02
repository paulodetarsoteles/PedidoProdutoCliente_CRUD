using PedidoProdutoCliente.Application.Services.ClienteServices;
using PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces;
using PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces;
using PedidoProdutoCliente.Infrastructure.Repository;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;
using PedidoProdutoCliente.Infrastructure.Transactions;
using PedidoProdutoCliente.Infrastructure.TransactionsInterfaces;
using PedidoProdutoproduto.Application.Services.produtoServices;
using PedidoProdutoProduto.Application.Services.ProdutoServices;

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

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoListarPaginadoService, ProdutoListarPaginadoService>();
            services.AddScoped<IProdutoBuscarPorNomeService, ProdutoBuscarPorNomeService>();
            services.AddScoped<IProdutoAdicionarService, ProdutoAdicionarService>();
            services.AddScoped<IProdutoAtualizarService, ProdutoAtualizarService>();
            services.AddScoped<IProdutoExcluirService, ProdutoExcluirService>();
        }
    }
}
