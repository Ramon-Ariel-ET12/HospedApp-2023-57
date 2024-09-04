using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class HotelController : Controller
    {
        private readonly IAdo _Hotel;
        public HotelController(IAdo ado) => _Hotel = ado;
        public async Task<IActionResult> Busqueda()
        {
            var listado = await _Hotel.ObtenerHotelAsync();
            return View(busqueda);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            IEnumerable<Hotel>? hotel = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                hotel = await _Hotel.BuscarHotelAsync(busqueda);
                if (hotel.Count() == 0)
                    return View("No Encontrado");
            }
            hotel = hotel ?? new List<Hotel>();
        }
    }
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
