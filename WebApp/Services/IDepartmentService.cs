using WebApp.Models.Entities;

namespace WebApp.Services
{
    public interface IDepartmentService
    {

        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department> CreateAsync(Department dto);
        Task<Department> UpdateAsync(int id, Department dto);
        Task<(bool Success, string? Error)> DeleteAsync(int id);
    }
}
