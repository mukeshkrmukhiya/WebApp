using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Entities;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService) : base()
        {
            this._departmentService = departmentService;
        }

        [HttpGet("{departmentId}/employees")]
        public async Task<IActionResult> EmployeesInSameDepartment(int departmentId)
        {
            var employees = await _departmentService.EmployeesInSameDepartment(departmentId);
            return Ok(employees);
        }



        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            var createdDepartment = await _departmentService.CreateAsync(department);
            if (createdDepartment == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            var updatedDepartment = await _departmentService.UpdateAsync(id, department);
            if (updatedDepartment == null)
            {
                return NotFound();
            }
            return Ok(updatedDepartment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var (success, error) = await _departmentService.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new { Message = error });
            }
            return NoContent();
        }


    }
}
