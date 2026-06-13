using WebApp.Models.Dtos;
using WebApp.Models.Entities;

namespace WebApp.Services
{
    public interface IEmployeeService 
    {
         Task<List<Employee>> GetAllEmployeesAsyc();
         Task<Employee?> GetEmployeeByIdAsync(Guid id);
         Task<Employee> AddEmployeeAsync(EmployeeDto employeeDto);
         Task<Employee?> UpdateEmployeeAsync(Guid id, EmployeeDto employeeDto);
         Task<bool> DeleteEmployeeAsync(Guid id);
    }
}
