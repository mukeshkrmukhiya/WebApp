using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Dtos;
using WebApp.Models.Entities;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService  = employeeService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Employee>>>  GetAllEmployees()
        {
            var employees =  await _employeeService.GetAllEmployeesAsyc();

            if(employees == null || employees.Count == 0)
                return NotFound();

            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if(employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            

            var employee = await _employeeService.AddEmployeeAsync(employeeDto);

            if (employeeDto == null)
                return BadRequest();

            return Ok(employee);
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateEmployee(Guid id, EmployeeDto employeeDto)
        {
            var  employee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);

            if (employeeDto == null)
                return BadRequest();

            return Ok(employee);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteEmployeeById(Guid id)
        {


            bool isDeleted = await _employeeService.DeleteEmployeeAsync(id);

            if(isDeleted == false)
                return NotFound();
        
            return NoContent();
        }
    }
}
