using System.ComponentModel.DataAnnotations;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class UpdateUserModel
    {
        [Required(ErrorMessage = "User Name é obrigatório!")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório!")]
        public string Email { get; set; }

    }
}
