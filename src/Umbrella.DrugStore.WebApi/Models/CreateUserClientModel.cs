﻿using System.ComponentModel.DataAnnotations;

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

        public IEnumerable<AddressViewModel> Address { get; set; } = new List<AddressViewModel>();
    }
}
