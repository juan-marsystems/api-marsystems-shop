using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary1.models;
using ClassLibrary1.Repositories;

namespace API_MarketSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        [Route("obtenerUsuario")]
        public IActionResult ObtenerUsuario(string email, string password)
        {
            var usuario = _usuariosRepository.ObtenerUsuario(email, password);
            if(usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [Route("insertarUsuario")]
        public IActionResult InsertarUsuario([FromBody] Usuario request) 
        {
            var usuarioInsertado = _usuariosRepository.InsertarUsuario(request.NameUser, request.SurnameUser, request.AgeUser, request.EmailUser, request.PassUser);
            return CreatedAtAction(nameof(ObtenerUsuario), new { email = usuarioInsertado.EmailUser, password = usuarioInsertado.PassUser }, usuarioInsertado);
        }
    }
}
