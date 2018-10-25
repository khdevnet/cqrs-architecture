namespace SW.Store.Core
{
    public interface IInitializer
    {
        int Order { get; }

        void Init();
    }
}
