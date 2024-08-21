using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PinewoodMvc.Models;
using PinewoodMvc.Services;

namespace PinewoodMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CustomerApiClient _customerApiClient;

    public HomeController(ILogger<HomeController> logger, CustomerApiClient customerApiClient)
    {
        _customerApiClient = customerApiClient;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var customers = await _customerApiClient.GetCustomersAsync();
        return View(customers);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    } 

    [HttpGet]
    public async Task<IActionResult> Form(int? id)
    {
        if (id == null)
        {
            return View("CustomerForm", new Customer());
        }
        else
        {
            var customer = await _customerApiClient.GetCustomerAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View("CustomerForm", customer);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View("CustomerForm", customer);
        }

        if (customer.Id == 0)
        {
            await _customerApiClient.AddCustomerAsync(customer);
        }
        else
        {
            await _customerApiClient.UpdateCustomerAsync(customer);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _customerApiClient.DeleteCustomerAsync(id);
        return RedirectToAction(nameof(Index));
    }
    
}
