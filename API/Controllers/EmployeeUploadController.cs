using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeUploadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeUploadController> _logger;

        public EmployeeUploadController(ApplicationDbContext context, ILogger<EmployeeUploadController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    using var package = new ExcelPackage(file.OpenReadStream());
                    var worksheet = package.Workbook.Worksheets[1];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Assuming the header is in the first row
                    {
                        var gpn = worksheet.Cells[row, 1].Value?.ToString();
                        var email = worksheet.Cells[row, 2].Value?.ToString();
                        var contactNumber = worksheet.Cells[row, 3].Value?.ToString();
                        var address = worksheet.Cells[row, 4].Value?.ToString();

                        // Basic validation: Ensure required fields are not empty
                        if (!string.IsNullOrEmpty(gpn) && !string.IsNullOrEmpty(email))
                        {
                            var employee = new Employee
                            {
                                GPN = gpn,
                                Email = email,
                                ContactNumber = contactNumber,
                                Address = address
                            };

                            _context.Employees.Add(employee);
                        }
                        else
                        {
                            _logger.LogWarning($"Invalid data for employee in row {row}");
                        }
                    }

                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Uploaded {rowCount - 1} employee records."); // Subtract 1 for the header
                }
                else
                {
                    _logger.LogWarning("No file provided or the file is empty.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred during file upload: {ex.Message}");
                return BadRequest("An error occurred during file upload.");
            }

            return Ok("File uploaded successfully");
        }
    }
}
