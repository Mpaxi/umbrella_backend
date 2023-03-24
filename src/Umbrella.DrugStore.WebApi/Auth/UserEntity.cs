using Microsoft.AspNetCore.Identity;

namespace Umbrella.DrugStore.WebApi.Auth
{
    public class UserEntity : IdentityUser
    {
        public string Nome { get; set; }
        public long CPF { get; set; }
    }
}
