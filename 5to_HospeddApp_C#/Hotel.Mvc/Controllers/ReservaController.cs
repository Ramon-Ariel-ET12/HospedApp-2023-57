using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IAdo _reserva;
        public ReservaController(IAdo ado) => _reserva = ado;
        public async Task<IActionResult> Listado()
        {
            var listado = await _reserva.ObtenerReservaAsync();
            return View(listado);
        }

    }
}
