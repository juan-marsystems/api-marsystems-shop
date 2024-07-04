using ClassLibrary1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary1.Repositories;

namespace API_MarketSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenesRepository _ordenesRepository;

        public OrdenesController(IOrdenesRepository ordenesRepository)
        {
            _ordenesRepository = ordenesRepository;
        }

        [HttpPost]
        [Route("realizarCompra")]
        public IActionResult RealizarCompra(int userId)
        {
            var exito = _ordenesRepository.RealizarCompra(userId);
            if (!exito)
            {
                return BadRequest("No se pudo realizar compra");
            }
            return Ok("Compra realizada con exito");
        }

        [HttpGet]
        [Route("obtenerOrdenesPorUsuario")]
        public IActionResult ObtenerOrdenesPorUsuario(int userId)
        {
            var ordenes = _ordenesRepository.ListarOrdenesPorUser(userId);
            if(ordenes == null || !ordenes.Any())
            {
                return NotFound("No hay ordenes");
            }
            return Ok(ordenes);
        }
    }
}
