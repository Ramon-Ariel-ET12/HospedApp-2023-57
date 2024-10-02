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
            var Busqueda = await _Cuarto.ObtenerCuarto_CamaAsync();
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
        public async Task<IActionResult> Upsert(Cuarto_Cama cuarto_cama)
        {
            try
            {
                if (cuarto_cama.IdCuarto == 0 || cuarto_cama.IdCuarto == null)
                    await _Cuarto.AltaCuartoAsync(cuarto_cama);
                else
                {
                    var existe = await _Cuarto.ObtenerCuartoPorIdAsync(cuarto_cama.IdCuarto);
                    if (existe is null)
                        return NotFound();

                    await _Cuarto.ModificarCuartoAsync(cuarto_cama);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
            return RedirectToAction("Busqueda");
        }
    }
}