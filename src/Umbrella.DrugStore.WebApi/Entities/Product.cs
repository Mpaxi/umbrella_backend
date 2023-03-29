namespace Umbrella.DrugStore.WebApi.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Unit { get; set; }
        public decimal Rating { get; set; }
    }
}
