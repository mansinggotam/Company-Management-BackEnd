using System.ComponentModel.DataAnnotations;

namespace BackEndDotNet.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Name {  get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public DateOnly? Birthdate { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}
