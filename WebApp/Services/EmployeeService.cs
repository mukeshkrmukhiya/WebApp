using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Dtos;
using WebApp.Models.Entities;

namespace WebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context): base()
        {
            this._context = context;
        }

       public async Task<List<Employee>> GetAllEmployeesAsyc()
        {
         return  await _context.Employees.ToListAsync();
            
        }
       public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }
       public async Task<Employee> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                Salary = employeeDto.Salary
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;

        }
       public async Task<Employee?> UpdateEmployeeAsync(Guid id, EmployeeDto employeeDto)
        {
            var employee =  await _context.Employees.FindAsync(id);

            if(employee == null)
            {
                return null;
            }

            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.Salary = employeeDto.Salary;
            await _context.SaveChangesAsync();
            return employee;
        }
       public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if(employee == null)
            {
                return false;
            }

             _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
