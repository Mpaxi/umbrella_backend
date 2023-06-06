namespace Umbrella.DrugStore.WebApi.Entities
{
    public enum EnumStatus
    {   
        AGUARDANDO_PAGAMENTO = 1,
        PAGAMNETO_APROVADO = 2,
        PAGAMENTO_REJEITADO = 3,
        AGUARDANDO_ENTREGA = 4,
        EM_TRANSPORTE = 5,
        ENTREGUE = 6
    }
}
