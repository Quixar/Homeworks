using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_les_proj.Models;
using ASP_les_proj.Models.Home;

namespace ASP_les_proj.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Razor()
    {
        HomeRazorPageModel model = new()
        {
            Arr = [ "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" ]
        };
        return View(model);
    }

    public IActionResult Demo()
    {
        List<DemoPageModel> model = new List<DemoPageModel>
        {
            new DemoPageModel { Name = "Product1", Price = 1000, Quantity = 5 },
            new DemoPageModel { Name = "Product2", Price = 2500, Quantity = 3 },
            new DemoPageModel { Name = "Product3", Price = 3500, Quantity = 7 }
        };

        return View(model);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}