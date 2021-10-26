using EmployeeTest.Data.Entity.EmployeeEntity;
using EmployeeTest.Service.EmployeeDataService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepoService _employeeRepo;

        public EmployeeController(IEmployeeRepoService employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        //GET:api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return Ok(await _employeeRepo.GetAllEmployees());
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int? id)
        {
            var Employee = await _employeeRepo.GetEmployee(id);

            if (Employee == null)
            {
                return NotFound();
            }

            return Employee;
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] Employee Employee)
        {
           

            try
            {
                
                return Ok(await _employeeRepo.SaveEmployee(Employee));
            }
            catch (DbUpdateConcurrencyException)
            {
                
             throw;
                
            }

            //return NoContent();
        }

        // POST: api/Employee
        //[HttpPost]
        //public async Task<IActionResult> SaveEmployee([FromForm]Employee Employee)
        //{

        //    return Ok(await _employeeRepo.SaveEmployee(Employee));

        //}

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteEmployee(int? id)
        {
            var Employee = await _employeeRepo.GetEmployee(id);
            if (Employee == null)
            {
                return NotFound();
            }

            return await _employeeRepo.DeleteEmployee(Employee);
        }

        private bool EmployeeExists(int? id)
        {
            var data =_employeeRepo.GetAllEmployeeData();
            return data.Any(e => e.Id == id);
        }
    }
}
