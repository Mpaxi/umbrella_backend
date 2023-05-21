using System.ComponentModel.DataAnnotations;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Rua do password é obrigatório!")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Numero do password é obrigatório!")]
        public int Numero { get; set; }

        //[Required(ErrorMessage = "Numero do password é obrigatório!")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Bairro do password é obrigatório!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cidade do password é obrigatório!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "UF do password é obrigatório!")]
        public string UF { get; set; }

        [Required(ErrorMessage = "CEP do password é obrigatório!")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Principal do password é obrigatório!")]
        public bool Principal { get; set; }
    }
}
