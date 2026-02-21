using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College.Data
{
    public class Student
    {
        
        
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string StudentName { get; set; }
        [Required]
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        public DateTime DOB { get; set; }
    }
}
