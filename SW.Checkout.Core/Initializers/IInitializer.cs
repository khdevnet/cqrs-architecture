namespace SW.Store.Core.Initializers
{
    public interface IInitializer
    {
        int Order { get; }

        void Init();
    }
}
