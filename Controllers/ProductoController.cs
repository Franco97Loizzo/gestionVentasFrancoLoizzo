using GestionVentasFRANCOLOIZZO.Models;
using GestionVentasFRANCOLOIZZO.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionVentasFRANCOLOIZZO.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private ProductoRepository repository = new ProductoRepository();

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Producto> lista = repository.listarProductos();
                return Ok("Productos" + lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
