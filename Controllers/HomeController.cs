using Microsoft.AspNetCore.Mvc;

public class HomeController:Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(string data)
    {
        return Content($"Data Received:{data}");
    }
}