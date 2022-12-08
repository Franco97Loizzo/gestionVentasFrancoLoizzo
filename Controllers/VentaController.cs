using GestionVentasFRANCOLOIZZO.Models;
using GestionVentasFRANCOLOIZZO.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionVentasFRANCOLOIZZO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : Controller
    {
        private VentaRepository repository = new VentaRepository();

        //listar ventas
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Venta> lista = repository.listarVentas();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        // obtener ventas por id
        [HttpGet("{id}")]
        public ActionResult<Venta> Get(long id)
        {
            try
            {
                Venta? venta = repository.obtenerVenta(id);
                if (venta != null)
                {
                    return Ok(venta);
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

        //eliminar venta
        [HttpDelete]
        public ActionResult Delete([FromBody] long id)
        {
            try
            {
                bool seElimino = repository.eliminarVenta(id);
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

        //crear venta
        [HttpPost]
        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                repository.crearVenta(venta);
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        //actualizar venta
        [HttpPut("{id}")]
        public ActionResult<Venta> Put(long id, [FromBody] Venta ventaAActualizar)
        {
            try
            {
                Venta? ventaActualizado = repository.actualizarVenta(id, ventaAActualizar);
                if (ventaActualizado != null)
                {
                    return Ok(ventaActualizado);
                }
                else
                {
                    return NotFound("no se encontro venta");
                }
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
