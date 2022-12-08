using GestionVentasFRANCOLOIZZO.Models;
using GestionVentasFRANCOLOIZZO.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionVentasFRANCOLOIZZO.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private ProductoRepository repository = new ProductoRepository();


        //listar productos
        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<Producto> lista = repository.listarProductos();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        // obtener producto por id
        [HttpGet("{id}")]
        public ActionResult<Producto> Get(long id)
        {
            try
            {
                Producto? producto = repository.obtenerProducto(id);
                if(producto != null)
                {
                    return Ok(producto);
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

        //eliminar producto
        [HttpDelete]
        public ActionResult Delete([FromBody]long id)
        {
            try
            {
                bool seElimino = repository.eliminarProducto(id);
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

        //crear producto
        [HttpPost]
        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                repository.crearProducto(producto);
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        //actualizar producto
        [HttpPut("{id}")]
        public ActionResult<Producto> Put(long id, [FromBody]Producto productoAActualizar)
        {
            try
            {
                Producto? productoActualizado = repository.actualizarProducto(id, productoAActualizar);
                if(productoActualizado != null)
                {
                    return Ok(productoActualizado);
                }
                else
                {
                    return NotFound("no se encontro producto");
                }
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

    }
}
