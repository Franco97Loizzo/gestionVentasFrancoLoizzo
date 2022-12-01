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
    }
}