using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Mvc.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IAdo _reserva;
        public ReservaController(IAdo ado) => _reserva = ado;

        [HttpGet]
        public async Task<IActionResult> Busqueda()
        {
            var Busqueda = await _reserva.ObtenerReservaAsync();
            return View(Busqueda);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            if (busqueda == null)
                return View("Busqueda", await _reserva.ObtenerReservaAsync());
            IEnumerable<Reserva>? reserva = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                reserva = await _reserva.BuscarReservaAsync(busqueda);
                if (reserva.Count() == 0)
                    return View("NoEncontrado");
            }
            reserva = reserva ?? new List<Reserva>();
            return View("Busqueda", reserva);
        }

        [HttpGet]
        public IActionResult Alta() => View("Upsert");

        [HttpGet]
        public async Task<IActionResult> Modificar(ushort? id)
        {
            if (id is null || id == 0)
                return NotFound();

            var reserva = await _reserva.ObtenerReservaPorIdAsync(id);
            if (reserva is null)
                return NotFound();

            return View("Upsert", reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Reserva reserva)
        {
            try
            {
                if (reserva.IdReserva == 0)
                    await _reserva.AltaReservaAsync(reserva);
                else
                {
                    var existe = await _reserva.ObtenerReservaPorIdAsync(reserva.IdReserva);
                    if (existe is null)
                        return NotFound();
                        
                    await _reserva.ModificarReservaAsync(reserva);
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
