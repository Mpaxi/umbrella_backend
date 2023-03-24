using System.ComponentModel.DataAnnotations;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório!")]
        public long CPF { get; set; }

        [Required(ErrorMessage = "IsAdmin é obrigatório!")]
        public bool IsAdmin { get; set; } = false;

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password é obrigatório!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmação do password é obrigatório!")]
        public string ConfirmPassword { get; set; }
    }
}
