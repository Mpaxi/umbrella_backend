using System.ComponentModel.DataAnnotations;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class UpdateUserModel
    {        
        [Required(ErrorMessage = "Id é obrigatório!")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "IsAdmin é obrigatório!")]
        public bool IsAdmin { get; set; } = false;

        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório!")]
        public long CPF { get; set; }

        [Required(ErrorMessage = "Password é obrigatório!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmação do password é obrigatório!")]
        public string ConfirmPassword { get; set; }

    }
}
