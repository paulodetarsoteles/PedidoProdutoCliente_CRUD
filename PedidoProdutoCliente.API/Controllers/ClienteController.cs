using Microsoft.AspNetCore.Mvc;
using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.API.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController(IClienteListarPaginadoService clienteListarPaginadoService) : ControllerBase
    {
        private readonly IClienteListarPaginadoService _clienteListarPaginadoService = clienteListarPaginadoService;

        [HttpGet("listar")]
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
                return StatusCode(500, new BaseResponse<List<Cliente>>(false, "Erro inesperado."));
            }
        }
    }
}
