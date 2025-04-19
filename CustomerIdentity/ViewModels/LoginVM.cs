using System.ComponentModel.DataAnnotations;

namespace CustomerIdentity.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Username is Required")]
        public string? Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Remember Me is Required")]
        public bool RememberMe { get; set; }    
    }
}
