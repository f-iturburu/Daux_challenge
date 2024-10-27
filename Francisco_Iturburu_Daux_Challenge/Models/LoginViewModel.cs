using System.ComponentModel.DataAnnotations;

namespace Francisco_Iturburu_Daux_Challenge.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 20 characters.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First name can only contain alphabetic characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 20 characters.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last name can only contain alphabetic characters.")]
        public string LastName { get; set; }
    }
}