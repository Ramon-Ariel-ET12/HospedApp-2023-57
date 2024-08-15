using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hotel.Mvc.Models;
using HotelApp.Dapper;
using HotelApp.Core;
using System.Data;

namespace Hotel.Mvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Reservas() => View();
    [HttpPost]
    public IActionResult Reservas(IAdo ado) 
    {
        List<Cliente> clientes = ado.ObtenerCliente();
        return View(clientes);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() 
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
