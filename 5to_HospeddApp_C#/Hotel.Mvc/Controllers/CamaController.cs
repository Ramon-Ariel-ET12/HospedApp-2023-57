using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Mvc.Controllers
{
    public class CamaController : Controller
    {
        private readonly IAdo _Cama;
        public CamaController(IAdo ado) => _Cama = ado;
        public async Task<IActionResult> Busqueda()
        {
            var busqueda = await _Cama.ObtenerCamaAsync();
            return View(busqueda);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            if (busqueda == null)
                return View("Busqueda", await _Cama.ObtenerCamaAsync());
            IEnumerable<Cama>? cama = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                cama = await _Cama.BuscarCamaAsync(busqueda);
                if (cama.Count() == 0)
                    return View("NoEncontrado");
            }
            cama = cama ?? new List<Cama>();
            return View("Busqueda", cama);
        }
        [HttpGet]
        public IActionResult Alta() => View("Upsert");
        [HttpGet]
        public async Task<IActionResult> Modificar(byte? id)
        {
            if (id is null || id == 0)
                return NotFound();

            var cama = await _Cama.ObtenerCamaPorIdAsync(id);
            if (cama is null)
                return NotFound();

            return View("Upsert", cama);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Cama cama)
        {
            try
            {
                if (cama.IdCama == null || cama.IdCama == 0)
                    await _Cama.AltaCamaAsync(cama);
                else
                {
                    var existe = await _Cama.ObtenerCamaPorIdAsync(cama.IdCama);
                    if (existe is null)
                        return NotFound();
                    await _Cama.ModificarCamaAsync(cama);
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
