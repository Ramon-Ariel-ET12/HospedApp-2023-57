using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IAdo _Cliente;
        public ClienteController(IAdo ado) => _Cliente = ado;
        public async Task<IActionResult> Listado()
        {
            var listado = await _Cliente.ObtenerClienteAsync();
            return View(listado);
        }

        /*[HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            IEnumerable<Cliente>? cliente = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                cliente = await _Cliente.(busqueda);
                if (cliente.Count() == 0)
                    return View("NoEncontrado");
            }
            cliente = cliente ?? new List<PersonaJuego>();
            return View("Busqueda", cliente);
        }*/
    }
}
