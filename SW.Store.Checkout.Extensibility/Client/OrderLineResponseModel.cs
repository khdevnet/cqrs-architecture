namespace SW.Store.Checkout.Extensibility.Client
{
    public class OrderLineResponseModel
    {
        public int ProductNumber { get; set; }

        public string ProductName { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
