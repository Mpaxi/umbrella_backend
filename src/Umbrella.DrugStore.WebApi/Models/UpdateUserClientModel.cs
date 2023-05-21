using System.ComponentModel.DataAnnotations;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class UpdateUserClientModel
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        [MinLength(3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Genero é obrigatório!")]
        public bool Masculino { get; set; }

        [Required(ErrorMessage = "DataNascimento é obrigatório!")]
        public DateTime DataNascimento { get; set; }

        public IEnumerable<AddressViewModel> Address { get; set; } = new List<AddressViewModel>();
    }
}
