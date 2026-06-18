using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Entities;


namespace WebApp.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _dbContext;
        private readonly IEmployeeService _employeeService;

        public DepartmentService(AppDbContext dbContext, IEmployeeService employeeService) : base()
        {
            this._dbContext = dbContext;
            this._employeeService = employeeService;
        }

        public async Task<object> EmployeesInSameDepartment(int departmentId)
        {
            var department = await _dbContext.Departments.FindAsync(departmentId);
            var employees = await _employeeService.GetAllEmployeesAsyc();
            if (department == null)
            {
                return null;
            }

            var result = employees
                .Where(e => e.DepartmentId == departmentId)
                .Select(e => new
                {
                    EmpId = e.Id,
                    EmpName = e.Name,
                    EmpEmail = e.Email,
                    EmpPhone = e.PhoneNumber,
                    EmpSalary = e.Salary,
                    EmpDepartment = department.Name
                }).ToList();

            return result;
        }



        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _dbContext.Departments.ToListAsync();

        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _dbContext.Departments.FindAsync(id);
        }
        public async Task<Department> CreateAsync(Department department)
        {
            if (department == null)
            {
                return null;
            }

            Department newDepartment = new Department
            {
                Name = department.Name
            };
            await _dbContext.Departments.AddAsync(newDepartment);
            await _dbContext.SaveChangesAsync();
            return newDepartment;
        }
        public async Task<Department> UpdateAsync(int id, Department department)
        {
            Department? existingDepartment = await _dbContext.Departments.FindAsync(id);

            if (existingDepartment == null)
            {
                return null;
            }

            existingDepartment.Name = department.Name;

            await _dbContext.SaveChangesAsync();
            return existingDepartment;



        }
        public async Task<(bool Success, string? Error)> DeleteAsync(int id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return (false, "Department not found");
            }
            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();

            return (true, null);
        }



    }
}
