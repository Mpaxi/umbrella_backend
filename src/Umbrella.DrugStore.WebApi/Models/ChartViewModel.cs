using Umbrella.DrugStore.WebApi.Entities;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class ChartViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public Chart ToChart(Guid clientId)
        {
            return new Chart
            {
                ClientId = clientId,
                Products = Products.Select(x => new ProductChart
                {
                    ProductId = x.Id,
                    Quantity = x.Quatity
                }).ToList()
            };
        }
    }
}
