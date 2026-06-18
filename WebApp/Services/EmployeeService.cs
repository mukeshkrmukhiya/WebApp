using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Dtos;
using WebApp.Models.Entities;

namespace WebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IDepartmentService _departmentService;

        public EmployeeService(AppDbContext context, IDepartmentService departmentService): base()
        {
            this._context = context;
            this._departmentService = departmentService;
        }


        public async Task<object> EmployeeFullDetailsAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            var departments = await _departmentService.GetAllAsync();

            var result = employees.Join(
                departments,
                employee => employee.DepartmentId,
                department => department.Id,
                (employee, department) => new
                {
                    EmpId = employee.Id,
                    EmpName = employee.Name,
                    EmpEmail = employee.Email,
                    EmpPhone = employee.PhoneNumber,
                    EmpSalary = employee.Salary,
                    EmpDepartment = department.Name
                }).ToList();

            return result;
        }



       public async Task<List<Employee>> GetAllEmployeesAsyc()
        {
         return  await _context.Employees
                //.Include(e => e.Department)
                .ToListAsync();
            
        }


        public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees
                //.Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
       public async Task<Employee> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                Salary = employeeDto.Salary,
                DepartmentId = employeeDto.DepartmentId
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
            employee.DepartmentId = employeeDto.DepartmentId;
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> IncreaseSalaryAsync( Guid id, IncreaseSalaryDto increaseSalaryDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return null;
            }
            employee.Salary += employee.Salary * (increaseSalaryDto.Percentage / 100);
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
