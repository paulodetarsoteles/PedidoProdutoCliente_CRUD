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

        [HttpGet("buscar-por-nome")]
        public async Task<IActionResult> BuscarPorNome([FromQuery] string nomeRequest)
        {
            try
            {
                var result = await _clienteBuscarPorNomeService.Process(nomeRequest);

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

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] ClienteRequest.Adicionar request)
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

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] ClienteRequest.Atualizar request)
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
