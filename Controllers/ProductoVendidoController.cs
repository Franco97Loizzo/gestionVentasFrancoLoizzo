using GestionVentasFRANCOLOIZZO.Models;
using GestionVentasFRANCOLOIZZO.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionVentasFRANCOLOIZZO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoVendidoController : Controller 
    {
        private ProductoVendidoRepository repository = new ProductoVendidoRepository();

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<ProductoVendido> lista = repository.listarProductoVendido();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        // obtener producto por id
        [HttpGet("{id}")]
        public ActionResult<ProductoVendido> Get(long id)
        {
            try
            {
                ProductoVendido? productoVendido = repository.obtenerProductoVendido(id);
                if (productoVendido != null)
                {
                    return Ok(productoVendido);
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
        public ActionResult Delete([FromBody] long id)
        {
            try
            {
                bool seElimino = repository.eliminarProductoVendido(id);
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
        public ActionResult Post([FromBody] ProductoVendido productoVendido)
        {
            try
            {
                repository.crearProductoVendido(productoVendido);
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        //actualizar producto
        [HttpPut("{id}")]
        public ActionResult<ProductoVendido> Put(long id, [FromBody] ProductoVendido productoVendidoAActualizar)
        {
            try
            {
                ProductoVendido? productoActualizado = repository.actualizarProductoVendido(id, productoVendidoAActualizar);
                if (productoActualizado != null)
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