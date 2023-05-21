using Microsoft.AspNetCore.Identity;

namespace Umbrella.DrugStore.WebApi.Auth
{
    public class UserEntity : IdentityUser
    {
        public string Nome { get; set; }
        public long CPF { get; set; }
        public bool Masculino { get; set; }
        public DateTime DataNascimento { get; set; }

        public string? Rua { get; set; }
        public int? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }
    }
}
