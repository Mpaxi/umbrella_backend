using Umbrella.DrugStore.WebApi.Auth;

namespace Umbrella.DrugStore.WebApi.Entities
{
    public class Order : BaseEntity
    {
        public int OrdeId { get; set; }
        public Guid UserId { get; set; }
        public EnumStatus Status { get; set; }
        public EnumPaymentType PaymentType { get; set; }
        public int Number { get; set; }
        public int CVV { get; set; }
        public string CardName { get; set; }
        public string ExpireAt { get; set; }
        public int Quota { get; set; }
        public string? Rua { get; set; }
        public int? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }
        public IList<OrderProduct> OrderProducts { get; set; }
    }
}
