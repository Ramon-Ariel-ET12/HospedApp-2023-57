using HotelApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Mvc.Controllers
{
    public class CuartoController : Controller
    {
        private readonly IAdo _Cuarto;
        public CuartoController(IAdo ado) => _Cuarto = ado;

        [HttpGet]
        public async Task<IActionResult> Busqueda()
        {
            var Busqueda = await _Cuarto.ObtenerCuartoAsync();
            return View(Busqueda);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? busqueda)
        {
            if (busqueda == null)
                return View("Busqueda", await _Cuarto.ObtenerCuartoAsync());
            IEnumerable<Cuarto>? cuarto = null;
            if (!string.IsNullOrEmpty(busqueda))
            {
                cuarto = await _Cuarto.BuscarCuartoAsync(busqueda);
                if (cuarto.Count() == 0)
                    return View("NoEncontrado");
            }
            cuarto = cuarto ?? new List<Cuarto>();
            return View("Busqueda", cuarto);
        }

        [HttpGet]
        public IActionResult Alta() => View("Upsert");

        [HttpGet]
        public async Task<IActionResult> Modificar(byte? id)
        {
            if (id is null || id == 0)
                return NotFound();

            var cuarto = await _Cuarto.ObtenerCuartoPorIdAsync(id);
            if (cuarto is null)
                return NotFound();

            return View("Upsert", cuarto);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Cuarto cuarto)
        {
            try
            {
                if (cuarto.IdCuarto == 0 || cuarto.IdCuarto == null)
                    await _Cuarto.AltaCuartoAsync(cuarto);
                else
                {
                    var existe = await _Cuarto.ObtenerCuartoPorIdAsync(cuarto.IdCuarto);
                    if (existe is null)
                        return NotFound();

                    await _Cuarto.ModificarCuartoAsync(cuarto);
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