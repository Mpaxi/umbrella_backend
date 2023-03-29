namespace Umbrella.DrugStore.WebApi.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; private set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool Active { get; set; } = true;
    }
}
