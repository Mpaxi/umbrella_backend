using System.ComponentModel.DataAnnotations;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class CreateUserClientModel
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        [MinLength(3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório!")]
        public long CPF { get; set; }

        [Required(ErrorMessage = "Genero é obrigatório!")]
        public bool Masculino { get; set; }

        [Required(ErrorMessage = "DataNascimento é obrigatório!")]
        public DateTime DataNascimento { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password é obrigatório!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmação do password é obrigatório!")]
        public string ConfirmPassword { get; set; }

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
    }
}
