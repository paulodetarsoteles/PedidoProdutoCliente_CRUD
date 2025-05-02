using Microsoft.AspNetCore.Mvc;
using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces;

namespace PedidoProdutoCliente.API.Controllers
{
    [ApiController]
    [Route("api/pedido")]
    public class PedidoController(IPedidoListarPaginadoService pedidoListarPaginadoService,
        IPedidoAdicionarService pedidoAdicionarService,
        IPedidoAtualizarService pedidoAtualizarService,
        IPedidoExcluirService pedidoExcluirService,
        ILogger<PedidoController> logger) : ControllerBase
    {
        private readonly IPedidoListarPaginadoService _pedidoListarPaginadoService = pedidoListarPaginadoService;
        private readonly IPedidoAdicionarService _pedidoAdicionarService = pedidoAdicionarService;
        private readonly IPedidoAtualizarService _pedidoAtualizarService = pedidoAtualizarService;
        private readonly IPedidoExcluirService _pedidoExcluirService = pedidoExcluirService;
        private readonly ILogger<PedidoController> _logger = logger;



        /// <summary>Lista os pedidos de forma paginada.</summary>
        /// <param name="request">Numero da página e a quantidade de resultados por página.</param>
        /// <returns>Retorna uma lista de pedidos.</returns>
        [HttpGet("listar-paginado")]
        public async Task<IActionResult> ListarPaginado([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _pedidoListarPaginadoService.Process(page, pageSize);

                if (result.ValidParameters == false) return BadRequest(result);

                if (result.Data == null || result.Data.Count == 0) return NotFound(result);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Adiciona um novo pedido.</summary>
        /// <param name="request">Dados do pedido a ser adicionado.</param>
        /// <returns>Retorna o resultado da operação.</returns>
        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] PedidoRequest.AdicionarPedidoRequest request)
        {
            try
            {
                var result = await _pedidoAdicionarService.Process(request);

                if (result.ValidParameters == false) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ocorreu um erro ao processar a requisição.");
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Atualiza um novo pedido.</summary>
        /// <param name="request">Dados do pedido a ser atualizado.</param>
        /// <returns>Retorna o resultado da operação.</returns>
        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] PedidoRequest.AtualizarPedidoRequest request)
        {
            try
            {
                var result = await _pedidoAtualizarService.Process(request);

                if (result.ValidParameters == false) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ocorreu um erro ao processar a requisição.");
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Exclui um pedido.</summary>
        /// <param name="request">Dados do pedido a ser excluído.</param>
        /// <returns>Retorna o resultado da operação.</returns>
        [HttpDelete("excluir")]
        public async Task<IActionResult> Excluir([FromQuery] int id)
        {
            try
            {
                var result = await _pedidoExcluirService.Process(id);

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
