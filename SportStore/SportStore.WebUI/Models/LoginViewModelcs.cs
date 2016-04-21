using System.ComponentModel.DataAnnotations;

namespace SportStore.WebUI.Models
{
    public class LoginViewModelcs
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}