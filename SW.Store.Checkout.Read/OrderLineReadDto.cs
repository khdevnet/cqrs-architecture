namespace SW.Store.Checkout.Read
{
    public class OrderLineReadDto
    {
        public int ProductNumber { get; set; }

        public string ProductName { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
