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
            if (busqueda == null)
                return View("Busqueda", await _Cliente.ObtenerClienteAsync());
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
        [HttpGet]
        public IActionResult Alta()
        {
            return View("Upsert");
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Cliente cliente)
        {
            var existe = await _Cliente.ObtenerClientePorDni(cliente.Dni);
            if (existe is null)
                await _Cliente.AltaClienteAsync(cliente);
            else
            {
                await _Cliente.ModificarClienteAsync(cliente);
            }
            return RedirectToAction("Busqueda");
        }
    }
}
