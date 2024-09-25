using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Mvc.Controllers
{
    public class Hotel_CuartoController : Controller
    {
        private readonly IAdo _Hotel_Cuarto;
        public Hotel_CuartoController(IAdo ado) => _Hotel_Cuarto = ado;

        [HttpGet]
        public async Task<IActionResult> Busqueda()
        {
            var Busqueda = await _Hotel_Cuarto.ObtenerCuartoAsync();
            return View(Busqueda);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            if (busqueda == null)
                return View("Busqueda", await _Hotel_Cuarto.ObtenerHotel_CuartoAsync());
            IEnumerable<Hotel_Cuarto>? hotel_cuarto = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                hotel_cuarto = await _Hotel_Cuarto.BuscarHotel_CuartoAsync(busqueda);
                if (hotel_cuarto.Count() == 0)
                    return View("NoEncontrado");
            }
            hotel_cuarto = hotel_cuarto ?? new List<Hotel_Cuarto>();
            return View("Busqueda", hotel_cuarto);
        }

        [HttpGet]
        public IActionResult Alta() => View("Upsert");

        [HttpGet]
        public async Task<IActionResult> Modificar(ushort? IdHotel, byte? IdCuarto)
        {
            if (IdHotel is null || IdHotel == 0)
                return NotFound();

            var existe = await _Hotel_Cuarto.ObtenerHotel_CuartoPorIdAsync(IdHotel, IdCuarto);
            if (existe is null)
                return NotFound();

            return View("Upsert", existe);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Hotel_Cuarto hotel_Cuarto)
        {
            try
            {
                if (hotel_Cuarto.Numero == 0 || hotel_Cuarto.Numero == null)
                    await _Hotel_Cuarto.AltaHotel_CuartoAsync(hotel_Cuarto);
                else
                {
                    var existe = await _Hotel_Cuarto.ObtenerHotel_CuartoPorIdAsync(hotel_Cuarto.IdHotel, hotel_Cuarto.IdCuarto);
                    if (existe is null)
                        return NotFound();

                    await _Hotel_Cuarto.ModificarHotel_Cuarto(hotel_Cuarto);
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