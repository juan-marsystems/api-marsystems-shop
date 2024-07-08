using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary1.Models;
using ClassLibrary1.Repositories;
using ClassLibrary1.DTO;

namespace API_MarketSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController : ControllerBase
    {
        private readonly ICarritosRepository _carritosRepository;

        public CarritosController(ICarritosRepository carritosRepository)
        {
            _carritosRepository = carritosRepository;
        }

        [HttpGet]
        [Route("obtenerArticulosCarrito")]
        public IActionResult ObtenerArticulosCarrito(int userId)
        {
            var articulos = _carritosRepository.ObtenerArticulosCarrito(userId);
            if (!articulos.Any())
            {
                return NotFound();
            }
            return Ok(articulos);
        }

        [HttpPost]
        [Route("insertarCarrito")]
        public IActionResult InsertarCarrito([FromBody] CarritoRequest request)
        {
            var nuevoCarrito = _carritosRepository.InsertarCarrito(request.IdUser, request.Detalles);
            if (nuevoCarrito == null)
            {
                return BadRequest("No se pudo insertar el carrito");
            }
            return Ok("Carrito guardado");
        }

        [HttpDelete]
        [Route("eliminarCarrito")]
        public IActionResult EliminarCarrito(int userId)
        {
            var exito = _carritosRepository.EliminarCarrito(userId);
            if (!exito)
            {
                return NotFound("No se encontro el carrito a eliminar");
            }
            return Ok("Carrito eliminado");
        }

        [HttpDelete]
        [Route("eliminarProductoPorId")]
        public IActionResult EliminarProductoPorId(int userId, int artId)
        {
            var exito = _carritosRepository.EliminarProductoPorId(userId, artId);
            if (!exito)
            {
                return BadRequest("No se pudo eliminar el producto del carrito");
            }
            return Ok("Producto eliminado del carrito");
        }

        [HttpPut]
        [Route("editarCarrito")]
        public IActionResult EditarCarrito([FromBody] CarritoRequest carritoRequest)
        {
            var exito = _carritosRepository.EditarCarrito(carritoRequest);
            if (!exito)
            {
                return NotFound("No se encontró carrito.");
            }
            return Ok("Carrito editado con éxito.");
        }
    }
}
