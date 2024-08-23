using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class HotelController : Controller
    {
        private readonly IAdo _hotel;
        public HotelController(IAdo ado) => _hotel = ado;
        public async Task<IActionResult> Listado()
        {
            var listado = await _hotel.ObtenerHotelAsync();
            return View(listado);
        }

    }
}
