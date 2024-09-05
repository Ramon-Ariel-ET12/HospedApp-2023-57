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
    }
}