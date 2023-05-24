namespace Umbrella.DrugStore.WebApi.Entities
{
    public class Chart : BaseEntity
    {
        public Guid? ClientId { get; set; }
        public IEnumerable<ProductChart> Products { get; set; } = new List<ProductChart>();
    }
}
