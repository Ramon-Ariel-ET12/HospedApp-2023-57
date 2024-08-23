using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IAdo _usuario;
        public UsuarioController(IAdo ado) => _usuario = ado;
        public async Task<IActionResult> Listado()
        {
            var listado = await _usuario.ObtenerClienteAsync();
            return View(listado);
        }

    }
}
