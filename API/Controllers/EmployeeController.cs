using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;

public class EmployeeController:Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(ApplicationDbContext context, ILogger<EmployeeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var employees = _context.Employees.ToList();
        return View(employees);
    }

    // [HttpPost]
    // public IActionResult Upload(IFormFile file)
    // {
    //     try
    //     {
    //         if (file != null && file.Length > 0)
    //         {
    //             using (var reader = new StreamReader(file.OpenReadStream()))
    //             using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
    //             {
    //                 var records = csv.GetRecords<Employee>().ToList();

    //                 foreach (var employee in records)
    //                 {
    //                     // Basic validation: Ensure email and GPN are not empty
    //                     if (!string.IsNullOrEmpty(employee.Email) && !string.IsNullOrEmpty(employee.GPN))
    //                     {
    //                         _context.Employees.Add(employee);
    //                     }
    //                     else
    //                     {
    //                         _logger.LogWarning($"Invalid data for employee: {employee}");
    //                     }
    //                 }

    //                 _context.SaveChanges();
    //                 _logger.LogInformation($"Uploaded {records.Count} employee records.");
    //             }
    //         }
    //         else
    //         {
    //             _logger.LogWarning("No file provided or the file is empty.");
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError($"An error occurred during file upload: {ex.Message}");
    //         TempData["ErrorMessage"] = "An error occurred during file upload.";
    //     }

    //     return RedirectToAction("Index");
    // }
    private bool IsValidEmail(string email)
    {
    // Implement email validation logic here
    // You can use regular expressions or other methods to validate the email format
    // For a simple example, checking if it contains '@' and '.' characters
    return email.Contains('@') && email.Contains('.');
    }

    private bool IsValidGPN(string gpn)
    {
    // Validate the GPN format: "IN" followed by 9 numeric digits
    return gpn.Length == 11 && gpn.StartsWith("IN") && gpn.Substring(2).All(char.IsDigit);
    }

}