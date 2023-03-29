using Umbrella.DrugStore.WebApi.Entities;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Unit { get; set; }
        public decimal Rating { get; set; }

        public Product toProduct()
        {
            return new Product{
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Unit = this.Unit,
                Rating = this.Rating
            };
        }
    }
}
