using WebApp.Models.Entities;

namespace WebApp.Models.Dtos
{
    public class EmployeeDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
    }
}
