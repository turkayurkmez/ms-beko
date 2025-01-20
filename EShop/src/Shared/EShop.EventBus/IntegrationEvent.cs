namespace EShop.EventBus
{
    public record IntegrationEvent
    {
        public Guid Id { get; init; }
        public DateTime CreationDate { get; init; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;

        }
    }
}
