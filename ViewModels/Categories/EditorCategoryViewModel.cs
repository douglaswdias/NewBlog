using System.ComponentModel.DataAnnotations;

namespace NewBlog.ViewModels.Categories
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Nome deve conter entre 3 e 40 caracteres")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Slug é obrigatório")]
        public string Slug { get; set; } = string.Empty;
    }
}
