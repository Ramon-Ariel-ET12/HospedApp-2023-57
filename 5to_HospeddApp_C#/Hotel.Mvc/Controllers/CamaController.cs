using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class CamaController : Controller
    {
        private readonly IAdo _cama;
        public CamaController(IAdo ado) => _cama = ado;
        public async Task<IActionResult> Listado()
        {
            var listado = await _cama.ObtenerCamaAsync();
            return View(listado);
        }

    }
}
