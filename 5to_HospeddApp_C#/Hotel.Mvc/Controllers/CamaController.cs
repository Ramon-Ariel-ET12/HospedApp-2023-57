using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Mvc.Controllers
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
        if (cama.IdCama == 0)
            await _Cama.RepoCama.AltaAsync(cama);
        else
        {
            var camaRepo = await _Cama.RepoCama.ObtenerPorIdAsync(cama.Id);
            if (camaRepo is null)
                return NotFound();
            camaRepo.Nombre = cama.Nombre;
            _Cama.RepoCama.Modificar(camaRepo);
        }
        try
        {
            await _Cama.GuardarAsync();
        }
        catch (EntidadDuplicadaException e)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Listado));
    }

    }
}
