using ClassLibrary1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_MarketSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticulosRepository _articulosRepository;

        public ArticulosController(IArticulosRepository articulosRepository)
        {
            _articulosRepository = articulosRepository;
        }

        [HttpGet]
        [Route("listarArticulos")]
        public IActionResult ListarArticulos()
        {
            var articulos = _articulosRepository.ListarArticulos();
            return Ok(articulos);
        }

        [HttpGet]
        [Route("obtenerArticuloPorId")]
        public IActionResult ObtenerArticuloPorId(int id)
        {
            var articulo = _articulosRepository.ObtenerArticuloPorId(id);
            if(articulo == null)
            {
                return NotFound();
            }
            return Ok(articulo);
        }
    }
}
