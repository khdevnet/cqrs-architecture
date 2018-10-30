namespace SW.Checkout.Core.Initializers
{
    public interface IInitializer
    {
        int Order { get; }

        void Init();
    }
}
