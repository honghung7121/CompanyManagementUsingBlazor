using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "This field is required and less than 20 chars")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Country { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
