using College.Validators;
using System.ComponentModel.DataAnnotations;

namespace College.Model
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "entered name is not valid")]
        [StringLength(30)]
        public string StudentName { get; set; }

        [Range(10, 20)]
        public int Age { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        [Compare (nameof(Password))]
        public string ConfirmPassword { get; set; }
        [DateCheck]
        public DateTime AdnissionDate { get; set; }
    }
}
