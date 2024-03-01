using System.ComponentModel.DataAnnotations;

namespace NewBlog.ViewModels.Accounts;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o E-mail")]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Informe a senha")]
    public string Password { get; set; }
}
