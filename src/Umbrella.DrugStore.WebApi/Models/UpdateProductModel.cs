using Umbrella.DrugStore.WebApi.Entities;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class UpdateProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Unit { get; set; }
        public Boolean Active { get; set; }
        public Product ToProduct()
        {
            return new Product
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Unit = this.Unit,
                Updated = DateTime.Now,
                Active = this.Active

            };
        }
    }
}
