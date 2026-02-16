using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hl7.Mvc.Models;
using Hl7.Core;

namespace Hl7.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new Hl7ParseViewModel());
    }

    [HttpPost]
    public IActionResult Index(Hl7ParseViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Hl7Message))
        {
            model.ErrorMessage = "Please enter an HL7 message.";
            return View(model);
        }

        try
        {
            var parser = new Hl7Parser();
            model.ParsedMessage = parser.ParseMessage(model.Hl7Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing HL7 message");
            model.ErrorMessage = $"Error parsing HL7 message: {ex.Message}";
        }

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
