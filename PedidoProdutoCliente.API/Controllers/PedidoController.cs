using Microsoft.AspNetCore.Mvc;
using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.ServicesInterfaces;

namespace PedidoProdutoCliente.API.Controllers
{
    [ApiController]
    [Route("api/pedido")]
    public class PedidoController(IClienteListarPaginadoService clienteListarPaginadoService) : ControllerBase
    {
        private readonly IClienteListarPaginadoService _clienteListarPaginadoService = clienteListarPaginadoService;

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
            catch (Exception)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] ClienteRequest.Adicionar request)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] ClienteRequest.Atualizar request)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }

        [HttpDelete("excluir")]
        public async Task<IActionResult> Excluir([FromQuery] int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }
    }
}
