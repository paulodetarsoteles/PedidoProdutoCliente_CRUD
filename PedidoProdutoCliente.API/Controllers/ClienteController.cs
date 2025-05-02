using Microsoft.AspNetCore.Mvc;
using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces;

namespace PedidoProdutoCliente.API.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController(IClienteBuscarPorNomeService clienteBuscarPorNomeService,
        IClienteListarPaginadoService clienteListarPaginadoService,
        IClienteAdicionarService clienteAdicionarService,
        IClienteAtualizarService clienteAtualizarService,
        IClienteExcluirService clienteExcluirService,
        ILogger<ClienteController> logger) : ControllerBase
    {
        private readonly IClienteBuscarPorNomeService _clienteBuscarPorNomeService = clienteBuscarPorNomeService;
        private readonly IClienteListarPaginadoService _clienteListarPaginadoService = clienteListarPaginadoService;
        private readonly IClienteAdicionarService _clienteAdicionarService = clienteAdicionarService;
        private readonly IClienteAtualizarService _clienteAtualizarService = clienteAtualizarService;
        private readonly IClienteExcluirService _clienteExcluirService = clienteExcluirService;
        private readonly ILogger<ClienteController> _logger = logger;

        /// <summary>Pesquisa um cliente pelo nome.</summary>
        /// <param name="request">Nome do cliente a ser pesquisado.</param>
        /// <returns>Retorna uma lista de clientes.</returns>
        [HttpGet("buscar-por-nome")]
        public async Task<IActionResult> BuscarPorNome([FromQuery] string nome)
        {
            try
            {
                var result = await _clienteBuscarPorNomeService.Process(nome);

                if (result.ValidParameters == false) return BadRequest(result);

                if (result.Data == null || result.Data.Count == 0) return NotFound(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ocorreu um erro ao processar a requisição.");
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Lista os clientes de forma paginada.</summary>
        /// <param name="request">Numero da página e a quantidade de resultados por página.</param>
        /// <returns>Retorna uma lista de clientes.</returns>
        [HttpGet("listar-paginado")]
        public async Task<IActionResult> ListarPaginado([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _clienteListarPaginadoService.Process(page, pageSize);

                if (result.ValidParameters == false) return BadRequest(result);

                if (result.Data == null || result.Data.Count == 0) return NotFound(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ocorreu um erro ao processar a requisição.");
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Adiciona um novo cliente.</summary>
        /// <param name="request">Dados do cliente a ser adicionado.</param>
        /// <returns>Retorna o resultado da operação.</returns>
        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] ClienteRequest.AdicionarClienteRequest request)
        {
            try
            {
                var result = await _clienteAdicionarService.Process(request);

                if (result.ValidParameters == false) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ocorreu um erro ao processar a requisição.");
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Atualiza um novo cliente.</summary>
        /// <param name="request">Dados do cliente a ser atualizado.</param>
        /// <returns>Retorna o resultado da operação.</returns>
        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] ClienteRequest.AtualizarClienteRequest request)
        {
            try
            {
                var result = await _clienteAtualizarService.Process(request);

                if (result.ValidParameters == false) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ocorreu um erro ao processar a requisição.");
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Exclui um cliente.</summary>
        /// <param name="request">Dados do cliente a ser excluído.</param>
        /// <returns>Retorna o resultado da operação.</returns>
        [HttpDelete("excluir")]
        public async Task<IActionResult> Excluir([FromQuery] int id)
        {
            try
            {
                var result = await _clienteExcluirService.Process(id);

                if (result == false) return BadRequest();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ocorreu um erro ao processar a requisição.");
                return StatusCode(500, "Erro inesperado");
            }
        }
    }
}
