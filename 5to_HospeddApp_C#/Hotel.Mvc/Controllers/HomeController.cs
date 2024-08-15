using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hotel.Mvc.Models;
using HotelApp.Core;

namespace Hotel.Mvc.Controllers;

public class HomeController : Controller
{
    readonly IAdo _ado;
    public HomeController(IAdo ado) => _ado = ado;
    
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Reservas() => View();
    /* se debe crear varios controladores de cada pagina con sus propias funciones
    [HttpPost]
    public IActionResult Reservas() 
    {
        List<Cliente> clientes = _ado.ObtenerCliente();
        return View(clientes);
    }*/
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() 
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
