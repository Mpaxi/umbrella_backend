namespace Umbrella.DrugStore.WebApi.Models
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
