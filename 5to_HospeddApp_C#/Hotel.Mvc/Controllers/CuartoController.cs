using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class CuartoController : Controller
    {
        private readonly IAdo _Cuarto;
        public CuartoController(IAdo ado) => _Cuarto = ado;
        public async Task<IActionResult> Listado()
        {
            var listado = await _Cuarto.ObtenerCuartoAsync();
            return View(listado);
        }

    }
}
