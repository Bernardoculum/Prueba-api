using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Probando_api.Models;

// AQUI SE CREAN LAS APIS

namespace Probando_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // Declaramos un objeto de dbContext
        public readonly DBAPIContext _dbContext;

        // Creamos el constructor
        public ProductoController(DBAPIContext _context)
        {
            // Inicializamos el contexto de la base de datos con el contexto inyectado
            _dbContext = _context;
        }
        // Hacemos una lista de todos los productos
        [HttpGet] // Este método responderá a las solicitudes HTTP GET
        [Route("Lista")] // La ruta de acceso para este método es "/Lista"
        public IActionResult Lista()
        {
            // Inicializamos una lista para contener los productos
            List<Producto> lista = new List<Producto>();

            try
            {
                // Intentamos obtener todos los productos de la base de datos y los almacenamos en la lista
                lista = _dbContext.Productos.Include(c => c.oCategoria ).ToList();

                // Devolvemos una respuesta HTTP 200 (OK) junto con un mensaje y la lista de productos
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, devolvemos una respuesta HTTP 200 (OK) con un mensaje de error y una lista vacía
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet] // Este método responderá a las solicitudes HTTP GET

        [Route("Obtener/{idProducto:int}")] // especificamos en la ruta que necesitamos un parametro


        public IActionResult Obtener(int idProducto)
        {
            // Inicializamos como un objeto de producto
            Producto oPruducto = _dbContext.Productos.Find(idProducto);

            //Realizamos una busqueda de producto si no lo encuentra, tira mensaje de producto no encontrado
            if (oPruducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {

                oPruducto = _dbContext.Productos.Include(c =>c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();
               

                // Devolvemos una respuesta HTTP 200 (OK) junto con un mensaje y la lista de productos
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oPruducto });
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, devolvemos una respuesta HTTP 200 (OK) con un mensaje de error y una lista vacía
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oPruducto });
            }
        }
    }
}
