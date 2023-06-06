using System.Runtime.InteropServices.ComTypes;
using Umbrella.DrugStore.WebApi.Auth;
using Umbrella.DrugStore.WebApi.Entities;

namespace Umbrella.DrugStore.WebApi.Models
{
    public class OrderCheckoutViewModel
    {
        public EnumPaymentType PaymentType { get; set; }
        public int Number { get; set; }
        public int CVV { get; set; }
        public string CardName { get; set; }
        public string ExpireAt { get; set; }
        public int Quota { get; set; }

        public IList<OrderProductViewModel> OrderProducts { get; set; }
        public Address Address { get; set; }
        public Order ToOrder(Guid userId)
        {
            var random = new Random();
            return new Order
            {
                PaymentType = PaymentType,
                Number = Number,
                CVV = CVV,
                CardName = CardName,
                ExpireAt = ExpireAt,
                Quota = Quota,
                OrdeId = random.Next(1, 999999),
                Status = EnumStatus.AGUARDANDO_PAGAMENTO,
                UserId = userId,
                Rua = Address.Rua,
                Numero = Address.Numero,
                Complemento = Address.Complemento,
                Bairro = Address.Bairro,
                Cidade = Address.Cidade,
                UF = Address.UF,
                CEP = Address.CEP,
                OrderProducts = OrderProducts.Select(o => new OrderProduct
                {
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                    UserId = userId
                }).ToList()
            };
        }
    }
}
