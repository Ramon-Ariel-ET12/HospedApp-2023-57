using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HotelApp.Mvc.Models;
using HotelApp.Core;

namespace HotelApp.Mvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() 
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
