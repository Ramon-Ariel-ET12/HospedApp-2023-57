using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class HotelController : Controller
    {
        private readonly IAdo _Hotel;
        private string? busqueda;
        public HotelController(IAdo ado) => _Hotel = ado;
        public async Task<IActionResult> Busqueda()
        {
            var busqueda = await _Hotel.ObtenerHotelAsync();
            return View(busqueda);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            IEnumerable<HotelApp.Core.Hotel>? hotel = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                hotel = await _Hotel.BuscarHotelAsync(busqueda);
                if (hotel.Count() == 0)
                    return View("NoEncontrado");
            }
            hotel = hotel ?? new List<HotelApp.Core.Hotel>();
            return View("Busqueda", hotel);
        }
    }
}
