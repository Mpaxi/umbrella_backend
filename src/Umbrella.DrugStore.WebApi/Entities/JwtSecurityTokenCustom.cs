using System.IdentityModel.Tokens.Jwt;

namespace Umbrella.DrugStore.WebApi.Entities
{
    public class JwtSecurityTokenCustom : JwtSecurityToken
    {
        public IList<String> Roles { get; set; }
    }
}
