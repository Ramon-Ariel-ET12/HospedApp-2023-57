using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Mvc.Controllers
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
        public IActionResult Alta() => View("Upsert");
        
        [HttpGet]
        public async Task<IActionResult> Modificar(uint? dni)
        {
            if (dni is null || dni == 0)
                return NotFound();

            var cliente = await _Cliente.ObtenerClientePorDni(dni);
            if (cliente is null)
                return NotFound();

            return View("Upsert", cliente);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Cliente cliente)
        {
            try
            {
                if (cliente.Dni == 0 || cliente.Dni == null)
                    await _Cliente.AltaClienteAsync(cliente);
                else
                {
                    var existe = await _Cliente.ObtenerClientePorDni(cliente.Dni);
                    if (existe is null)
                        return NotFound();
                    await _Cliente.ModificarClienteAsync(cliente);
                }
            }
            catch
            {
                return NotFound();
            }
            return RedirectToAction("Busqueda");
        }
    }
}