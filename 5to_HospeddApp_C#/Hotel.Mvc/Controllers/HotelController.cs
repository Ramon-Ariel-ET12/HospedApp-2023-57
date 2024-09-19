using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Mvc.Controllers
{
    public class HotelController : Controller
    {
        private readonly IAdo _hotel;
        public HotelController(IAdo ado) => _hotel = ado;

        [HttpGet]
        public async Task<IActionResult> Busqueda()
        {
            var Busqueda = await _hotel.ObtenerHotelAsync();
            return View(Busqueda);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            if (busqueda == null)
                return View("Busqueda", await _hotel.ObtenerHotelAsync());
            IEnumerable<Hotel>? hotel = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                hotel = await _hotel.BuscarHotelAsync(busqueda);
                if (hotel.Count() == 0)
                    return View("NoEncontrado");
            }
            hotel = hotel ?? new List<Hotel>();
            return View("Busqueda", hotel);
        }

        [HttpGet]
        public IActionResult Alta() => View("Upsert");

        [HttpGet]
        public async Task<IActionResult> Modificar(byte? id)
        {
            if (id is null || id == 0)
                return NotFound();

            var hotel = await _hotel.ObtenerHotelPorIdAsync(id);
            if (hotel is null)
                return NotFound();

            return View("Upsert", hotel);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Hotel hotel)
        {
            try
            {
                if (hotel.IdHotel == 0)
                    await _hotel.AltaHotelAsync(hotel);
                else
                {
                    var existe = await _hotel.ObtenerHotelPorIdAsync(hotel.IdHotel);
                    if (existe is null)
                        return NotFound();
                    await _hotel.ModificarHotelAsync(hotel);
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