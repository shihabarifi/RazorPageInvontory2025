using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageInvontory.Models;

namespace RazorPageInvontory.Pages
{
    public class EmployeeModel : PageModel
    {
        public class Employees
        {
            public string? Name { get; set; }
            public decimal Salary { get; set; }
        }
        [BindProperty]
        public Employees Employee { get; set; } = new Employees();


        public IActionResult OnPostSaveEmployee([FromBody] Employees employeeData)
        {
            if (employeeData == null || string.IsNullOrWhiteSpace(employeeData.Name) || employeeData.Salary <= 0)
            {
                return BadRequest("Invalid employee data.");
            }

            string message = $"Employee {employeeData.Name} with salary {employeeData.Salary:C} has been added successfully.";
            return new JsonResult(new { message });
        }
    }
}
