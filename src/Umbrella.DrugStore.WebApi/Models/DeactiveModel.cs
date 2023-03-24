using System.ComponentModel.DataAnnotations;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class DeactiveModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório!")]
        public string? Email { get; set; }
    }
}
