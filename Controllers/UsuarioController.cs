using GestionVentasFRANCOLOIZZO.Models;
using GestionVentasFRANCOLOIZZO.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionVentasFRANCOLOIZZO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepository repository = new UsuarioRepository();

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Usuario> lista = repository.listarUsuarios();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(long id)
        {
            try
            {
                Usuario? usuario = repository.obtenerUsuario(id);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        
        //eliminar usuario
        [HttpDelete]
        public ActionResult Delete([FromBody] int id)
        {
            try
            {
                bool seElimino = repository.eliminarUsuario(id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        
        //crear usuario
        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                repository.crearUsuario(usuario);
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        
        //actualizar producto
        [HttpPut("{id}")]
        public ActionResult<Usuario> Put(long id, [FromBody] Usuario usuarioAActualizar)
        {
            try
            {
                Usuario? usuarioActualizado = repository.actualizarUsuario(id, usuarioAActualizar);
                if (usuarioActualizado != null)
                {
                    return Ok(usuarioActualizado);
                }
                else
                {
                    return NotFound("no se encontro usuario");
                }
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
