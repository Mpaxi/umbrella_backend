using Microsoft.AspNetCore.Identity;

namespace Umbrella.DrugStore.WebApi.Auth
{
    public class UserEntity : IdentityUser
    {
        public string Nome { get; set; }
        public long CPF { get; set; }
        public bool Masculino { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<Address> Address { get; set; } = new List<Address>();
    }
}
