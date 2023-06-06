using System.ComponentModel.DataAnnotations;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class OrderProductViewModel
    {
        [Required(ErrorMessage = "Id do produto é obrigatório!")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Quantidade é obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero!")]
        public int Quantity { get; set; }
    }
}
