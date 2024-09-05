using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IAdo _Cliente;
        public ClienteController(IAdo ado) => _Cliente = ado;
        public async Task<IActionResult> Busqueda()
        {
            var busqueda = await _Cliente.ObtenerClienteAsync();
            return View(busqueda);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            IEnumerable<Cliente>? cliente = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                cliente = await _Cliente.BuscarClienteAsync(busqueda);
                if (cliente.Count() == 0)
                    return View("NoEncontrado");
            }
            cliente = cliente ?? new List<Cliente>();
            return View("Busqueda", cliente);
        }
    }
}