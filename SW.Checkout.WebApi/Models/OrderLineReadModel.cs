namespace SW.Checkout.WebApi.Models
{
    public class OrderLineReadModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }

    }
}
