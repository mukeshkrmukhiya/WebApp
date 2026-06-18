using System.Text.Json.Serialization;

namespace WebApp.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public required string Name { get; set; } 

        public required string Email { get; set; } 

        public string? PhoneNumber { get; set; }

        public required decimal Salary { get; set; }

        // Foreign Key
        public int? DepartmentId { get; set; }

        // Navigation Property
                        
        public Department? Department { get; set; }

    }
}
