namespace SW.Store.Core.Settings.Dto
{
    public class CommandBusSettings
    {
        public string Host { get; set; }

        public string QueueName { get; set; }

        public string Route { get; set; }

        public string Version { get; set; }
    }
}
