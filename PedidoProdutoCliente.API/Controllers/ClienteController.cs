using Microsoft.AspNetCore.Mvc;

namespace PedidoProdutoCliente.API.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        public ClienteController()
        {
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
