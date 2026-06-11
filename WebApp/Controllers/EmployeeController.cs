using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Dtos;
using WebApp.Models.Entities;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            this._context = context;
        }


        [HttpGet]
        public async Task<ActionResult>  GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            if(employeeDto == null) 
                return BadRequest();

            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                Salary = employeeDto.Salary
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return Ok(employee);
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateEmployee(Guid id, EmployeeDto employeeDto)
        {
            

            var  employee = await  _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            employee.Name = employeeDto.Name;
            employee. Email = employeeDto.Email;
            employee. PhoneNumber = employeeDto.PhoneNumber;
            employee.Salary = employeeDto.Salary;



            await _context.SaveChangesAsync();

            return Ok(employee);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteEmployeeById(Guid id)
        {


            var employee = await _context.Employees.FindAsync(id);

            if(employee == null)
                return NotFound();
            
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
