namespace Umbrella.DrugStore.WebApi.Entities
{
    public class OrderProduct : BaseEntity
    {
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; internal set; }
    }
}
