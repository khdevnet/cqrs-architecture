namespace SW.Checkout.Client.Models
{
    public class OrderLineResponseModel
    {
        public int ProductId { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }

    }
}
