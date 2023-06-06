using Umbrella.DrugStore.WebApi.Entities;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class UpdateOrderStatusViewModel
    {
        public Guid OrderId { get; set; }
        public EnumStatus Status { get; set; }
    }
}
