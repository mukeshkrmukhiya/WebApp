using System.Text.Json.Serialization;

namespace WebApp.Models.Entities
{
    public class Department
    {
       public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
        = new List<Employee>();
        

    }
}
