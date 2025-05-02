using Microsoft.AspNetCore.Mvc;
using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces;

namespace PedidoProdutoCliente.API.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController(IProdutoBuscarPorNomeService produtoBuscarPorNomeService,
        IProdutoListarPaginadoService produtoListarPaginadoService,
        IProdutoAdicionarService produtoAdicionarService,
        IProdutoAtualizarService produtoAtualizarService,
        IProdutoExcluirService produtoExcluirService,
        ILogger<ProdutoController> logger) : ControllerBase
    {
        private readonly IProdutoBuscarPorNomeService _produtoBuscarPorNomeService = produtoBuscarPorNomeService;
        private readonly IProdutoListarPaginadoService _produtoListarPaginadoService = produtoListarPaginadoService;
        private readonly IProdutoAdicionarService _produtoAdicionarService = produtoAdicionarService;
        private readonly IProdutoAtualizarService _produtoAtualizarService = produtoAtualizarService;
        private readonly IProdutoExcluirService _produtoExcluirService = produtoExcluirService;
        private readonly ILogger<ProdutoController> _logger = logger;

        [HttpGet("buscar-por-nome")]
        public async Task<IActionResult> BuscarPorNome([FromQuery] string nome)
        {
            try
            {
                var result = await _produtoBuscarPorNomeService.Process(nome);

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
                var result = await _produtoListarPaginadoService.Process(page, pageSize);

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
        public async Task<IActionResult> Adicionar([FromBody] ProdutoRequest.AdicionarProdutoRequest request)
        {
            try
            {
                var result = await _produtoAdicionarService.Process(request);

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
        public async Task<IActionResult> Atualizar([FromBody] ProdutoRequest.AtualizarProdutoRequest request)
        {
            try
            {
                var result = await _produtoAtualizarService.Process(request);

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
                var result = await _produtoExcluirService.Process(id);

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
