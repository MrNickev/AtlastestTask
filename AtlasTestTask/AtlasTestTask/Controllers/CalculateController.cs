using System.ComponentModel.DataAnnotations;
using AtlasTestTask.Models;
using AtlasTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtlasTestTask.Controllers;

public class CalculateController : Controller
{
    private readonly IPaymentScheduleService _scheduleService;

    public CalculateController(IPaymentScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Result(CreditModel credit)
    {
        Console.WriteLine($"{credit.Value} {credit.Term} {credit.Rate}");
        var context = new ValidationContext(credit, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(credit, context, validationResults, true);
        Console.WriteLine(isValid);
        if (!isValid)
        {
            ViewBag.ErrorMessage = "Данные формы не верные";
            return View("Index");
        }
        else
        {
            return View(_scheduleService.CreateScheduleByCredit(credit));
        }
    }
}