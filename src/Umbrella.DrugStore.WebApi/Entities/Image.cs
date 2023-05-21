namespace Umbrella.DrugStore.WebApi.Entities
{
    public class Image : BaseEntity
    {
        public string Source { get; set; }
        public EnumImageType Type { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
