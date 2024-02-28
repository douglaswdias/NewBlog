using System.ComponentModel.DataAnnotations;

namespace NewBlog.ViewModels;
public class RegisterViewModel
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string Email { get; set; }
}
