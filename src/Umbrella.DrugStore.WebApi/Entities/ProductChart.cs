namespace Umbrella.DrugStore.WebApi.Entities
{
    public class ProductChart : BaseEntity
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid ChartId { get; set; }
        public Chart Chart { get; set; }
    }
}
