using System.ComponentModel.DataAnnotations;
using AtlasTestTask.Models;
using AtlasTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtlasTestTask.Controllers;

public class CalculateDailyController : Controller
{
    
    private readonly IPaymentScheduleService _scheduleService;

    public CalculateDailyController(IPaymentScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    } 
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Result(CreditDailyModel creditDaily)
    {
        Console.WriteLine($"{creditDaily.Value} {creditDaily.Term} {creditDaily.Rate}");
        var context = new ValidationContext(creditDaily, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(creditDaily, context, validationResults, true);
        Console.WriteLine(isValid);
        if (!isValid)
        {
            ViewBag.ErrorMessage = "Данные формы не верные";
            return View("Index");
        }
        else
        {
            return View(_scheduleService.CreateScheduleByCredit(creditDaily));
        }
    }
    
}