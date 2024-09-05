using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IAdo _Reserva;
        public ReservaController(IAdo ado) => _Reserva = ado;
        [HttpGet]
        public async Task<IActionResult> Busqueda()
        {
            var busqueda = await _Reserva.ObtenerReservaAsync();
            return View(busqueda);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            IEnumerable<Reserva>? reserva = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                reserva = await _Reserva.BuscarReservaAsync (busqueda);
                if (reserva.Count() == 0)
                    return View("NoEncontrado");
            }
            reserva = reserva ?? new List<Reserva>();
            return View("Busqueda", reserva);
        }
    }
}
